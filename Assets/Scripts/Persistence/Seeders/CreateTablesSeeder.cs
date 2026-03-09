using Gun.Persistence;
using Ship.Persistence;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine.Android;
using UnityEngine.Rendering;

namespace Persistence.Seeders
{
    public class CreateTablesSeeder : ISaveSlotSeeder
    {
        public void Seed(SQLiteConnection connection)
        {
            // Gun
            connection.CreateTable<GunRackEntity>();
            connection.CreateTable<ShipEntity>();
        }
    }
}