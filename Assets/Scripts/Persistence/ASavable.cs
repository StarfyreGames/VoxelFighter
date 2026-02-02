using Persistence.Scripts;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Persistence
{
    public abstract class ASavable
    {
        public void Save()
        {
            SaveSlotManager.Database.Update(this);
        }
    }
}