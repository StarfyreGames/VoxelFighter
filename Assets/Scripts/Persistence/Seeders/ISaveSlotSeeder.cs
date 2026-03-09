using Unity.VisualScripting.Dependencies.Sqlite;

namespace Persistence.Seeders
{
    public interface ISaveSlotSeeder
    {
        public void Seed(SQLiteConnection connection);
    }
}