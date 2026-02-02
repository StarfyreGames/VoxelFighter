using System.Collections.Generic;
using Gun.Persistence;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Persistence.Seeders
{
    public class PlayerInitialWeaponLayoutSeeder : ISaveSlotSeeder
    {
        public void Seed(SQLiteConnection connection)
        {
            connection.Insert(CreateDefaultGun(1));
            connection.Insert(CreateDefaultGun(5));

            connection.Insert(CreateDefaultGunLayout());
        }

        private GunLayoutSave CreateDefaultGunLayout()
        {
            return new GunLayoutSave { Owner = "Player" };
        }

        private GunSlotSave CreateDefaultGun(int slot)
        {
            return new GunSlotSave
            {
                Slot = slot,
                Owner = "Player",
                BulletAssetName = "PC_8",
                FireModeAssetName = "PC_BaseFireMode",
                GunAssetName = "PulseCannon"
            };
        }
    }
}