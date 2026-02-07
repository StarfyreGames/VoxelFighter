using Gun.Persistence;
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
            connection.CreateTable<BulletEntity>();
            connection.CreateTable<FireModeEntity>();
            connection.CreateTable<GunSlotEntity>();
            connection.CreateTable<GunRackEntity>();
            connection.CreateTable<ShipEntity>();
        }
    }
}