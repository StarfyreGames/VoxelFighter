using Gun.Persistence;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Persistence.Seeders
{
    public class CreateTablesSeeder : ISaveSlotSeeder
    {
        public void Seed(SQLiteConnection connection)
        {
            connection.CreateTable<GunSlotSave>();
            connection.CreateTable<GunLayoutSave>();
        }
    }
}