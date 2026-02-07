using System.Collections.Generic;
using Gun.Persistence;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Persistence.Seeders
{
    public class PlayerShipSeeder : ISaveSlotSeeder
    {
        public void Seed(SQLiteConnection connection)
        {
            var gunRack = new GunRackEntity();
            gunRack.AddGun(CreateDefaultGun(1));
            gunRack.AddGun(CreateDefaultGun(5));

            var ship = new ShipEntity { ID = IShipRepository.PlayerId, Name = "Player" };
            ship.GunRack.Set(gunRack);

            ship.Save();
        }

        private static GunSlotEntity CreateDefaultGun(int slot)
        {
            var gunSlot = new GunSlotEntity { Slot = slot, WeaponPrefab = "PulseCannon" };
            gunSlot.FireMode.Set(IFireModeRepository.PulseCannonDefault());
            gunSlot.Bullet.Set(IBulletEntityRepository.CreatePulseCannon8());

            return gunSlot;
        }
    }
}