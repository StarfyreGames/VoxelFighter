using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Persistence.Scripts;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;

namespace Persistence.Savable
{
    public abstract class BaseSavable
    {
        public abstract int ID { get; set; }

        public abstract void Save();

        // I need this to be public for reasons - but use Save()
        public abstract void InternalPerformSaveYouDontWantThisOne();
    }

    public interface ISavableRepository<T> where T : BaseSavable, new()
    {
        static SQLiteConnection Database => SaveSlotManager.Database;
        static TableQuery<T> Table => Database.Table<T>();

        public static T GetById(int id) => Table.FirstOrDefault(entity => entity.ID == id);
    }

    public abstract class Savable<T> : BaseSavable, ISavableRepository<T>
        where T : Savable<T>, new()
    {
        private static SQLiteConnection Database => SaveSlotManager.Database;
        protected static TableQuery<T> Table => Database.Table<T>();

        private readonly Dictionary<string, IOwnedReference> _childToParentRelationships = new();
        private readonly Dictionary<string, IOwnerReference> _parentToChildRelationships = new();

        private int _id;

        [PrimaryKey, AutoIncrement]
        public override int ID
        {
            get => _id;
            set
            {
                _id = value;
                foreach (var relationship in _childToParentRelationships)
                    relationship.Value.UpdateOwnerId(value);
            }
        }

        public override void Save() => Database.RunInTransaction(InternalPerformSaveYouDontWantThisOne);

        // I need this to be public for reasons - but use Save()
        public override void InternalPerformSaveYouDontWantThisOne()
        {
            if (Database.Update(this) == 0)
                Database.Insert(this);

            // If we have loaded anything into memory, save that too.
            foreach (var pair in _parentToChildRelationships)
            {
                pair.Value.UpdateLink();
                pair.Value.TrySave();
            }
        }

        protected BelongsTo<TParent> BelongsTo<TParent>(string key, Func<int> getParentId, Action<int> setParentId)
            where TParent : BaseSavable, ISavableRepository<TParent>, new()
        {
            if (_childToParentRelationships.TryGetValue(key, out var parentRelationship))
                return (BelongsTo<TParent>)parentRelationship;

            var relationship = new BelongsTo<TParent>(getParentId, setParentId);
            _childToParentRelationships.Add(key, relationship);

            return relationship;
        }

        protected HasOne<TChild> HasOne<TChild>(string key, Func<TChild> getChild, Action<TChild> updateBackRef)
            where TChild : BaseSavable, ISavableRepository<TChild>, new()
        {
            return (HasOne<TChild>)GetParentRelationship(key, () => new HasOne<TChild>(getChild, updateBackRef));
        }

        protected HasMany<TChild> HasMany<TChild>(string key, Func<List<TChild>> getChildren,
            Action<TChild> updateBackRef)
            where TChild : BaseSavable, ISavableRepository<TChild>, new()
        {
            return (HasMany<TChild>)GetParentRelationship(key, () => new HasMany<TChild>(getChildren, updateBackRef));
        }

        private IOwnerReference GetParentRelationship(string key, Func<IOwnerReference> makeRelationship)
        {
            if (_parentToChildRelationships.TryGetValue(key, out var childRelationship))
                return childRelationship;

            var relationship = makeRelationship();
            _parentToChildRelationships.Add(key, relationship);

            return relationship;
        }
    }
}