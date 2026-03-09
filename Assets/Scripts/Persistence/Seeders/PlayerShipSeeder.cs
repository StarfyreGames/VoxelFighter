using Gun.Persistence;
using Ship.Persistence;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Persistence.Seeders
{
    public class PlayerShipSeeder : ISaveSlotSeeder
    {
        public void Seed(SQLiteConnection connection)
        {
            var gunRack = new GunRackEntity
            {
                WeaponBlueprintName = "Pulse Cannon (8)"
            };

            var ship = new ShipEntity { ID = IShipRepository.PlayerShipId, Name = "Player" };
            ship.GunRack.Set(gunRack);

            ship.Save();
        }
    }
}