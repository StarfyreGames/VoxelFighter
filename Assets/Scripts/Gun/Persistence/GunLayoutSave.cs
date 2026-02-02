using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Persistence;
using Persistence.Scripts;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Gun.Persistence
{
    public class GunLayoutSave : ASavable
    {
        [PrimaryKey] public string Owner { get; set; }


        private GunSlotSave[] _gunsCache;

        public GunSlotSave[] Guns()
        {
            return _gunsCache ??= GunSlotSave.FindByOwner(Owner);
        }

        [CanBeNull]
        public static GunLayoutSave LoadForOwner(string owner)
        {
            return SaveSlotManager.Database
                .Table<GunLayoutSave>()
                .FirstOrDefault(s => s.Owner == owner);
        }

        public static GunLayoutSave LoadForPlayer()
        {
            return LoadForOwner("Player");
        }
    }
}