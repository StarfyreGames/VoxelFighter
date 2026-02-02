using System.Linq;
using Persistence.Scripts;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Gun.Persistence
{
    public class GunSlotSave
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        [Indexed]
        public string Owner { get; set; }

        public int Slot  { get; set; }
        public string GunAssetName { get; set; }
        public string BulletAssetName { get; set; }
        public string FireModeAssetName { get; set; }

        public static GunSlotSave[] FindByOwner(string owner)
        {
            return SaveSlotManager.Database
                .Table<GunSlotSave>()
                .Where(save => save.Owner == owner)
                .ToArray();
        }
    }
}