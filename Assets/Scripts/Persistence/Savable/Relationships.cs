using System;
using System.Collections.Generic;
using Gun.Persistence;

namespace Persistence.Savable
{
    public interface IRelationship
    {
        public void TrySave();
    }

    public interface IOwnedReference : IRelationship
    {
        public void UpdateOwnerId(int id);
    }

    public interface IOwnerReference : IRelationship
    {
        public void UpdateLink();
    }

    public class BelongsTo<T> : IOwnedReference
        where T : BaseSavable, ISavableRepository<T>, new()
    {
        private readonly Func<int> _getParentId;
        private readonly Action<int> _setParentId;

        private T _value;

        public BelongsTo(Func<int> getParentId, Action<int> setParentId)
        {
            _getParentId = getParentId;
            _setParentId = setParentId;
        }

        public T Resolve()
        {
            _value ??= ISavableRepository<T>.GetById(_getParentId());
            return _value;
        }

        public void Set(T value)
        {
            _value = value;
            _setParentId(_value.ID);
        }

        public void UpdateOwnerId(int id)
        {
            _setParentId(id);
        }

        public void TrySave()
        {
            _value?.InternalPerformSaveYouDontWantThisOne();
        }

        public void UpdateLink(BaseSavable savable)
        {
            _setParentId(savable.ID);
        }
    }

    public class HasOne<T> : IOwnerReference
        where T : BaseSavable, ISavableRepository<T>, new()
    {
        private readonly Func<T> _getChild;
        private readonly Action<T> _updateBackRef;

        private T _child;

        public HasOne(Func<T> getChild, Action<T> updateBackRef)
        {
            _getChild = getChild;
            _updateBackRef = updateBackRef;
        }

        public T Resolve()
        {
            _child ??= _getChild();
            return _child;
        }

        public void Set(T child)
        {
            _child = child;
            _updateBackRef(_child);
        }

        public void TrySave()
        {
            _child?.InternalPerformSaveYouDontWantThisOne();
        }

        public void UpdateLink()
        {
            _updateBackRef(_child);
        }
    }

    public class HasMany<T> : IOwnerReference
        where T : BaseSavable, ISavableRepository<T>, new()
    {
        private readonly Func<List<T>> _getChildren;
        private readonly Action<T> _updateBackRef;

        private List<T> _children;

        public HasMany(Func<List<T>> getChildren, Action<T> updateBackRef)
        {
            _getChildren = getChildren;
            _updateBackRef = updateBackRef;
        }

        public List<T> Resolve()
        {
            _children ??= _getChildren();
            return _children;
        }

        public void Set(List<T> child)
        {
            _children = child;
            _children.ForEach(_updateBackRef);
        }

        public void Add(T child)
        {
            _children ??= new List<T>();
            _children.Add(child);
        }

        public void TrySave()
        {
            _children?.ForEach(child => child.InternalPerformSaveYouDontWantThisOne());
        }

        public void UpdateLink()
        {
            _children.ForEach(_updateBackRef);
        }
    }
}