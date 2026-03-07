using System.Collections.Generic;
using System.Linq;
using Persistence;
using Persistence.Savable;
using Ship.Persistence;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Gun.Persistence
{
    public class GunRackEntity : Savable<GunRackEntity>, IGunRackRepository
    {
        // === Relationships

        [Indexed(Unique = false)] public int ShipId { get; set; }
        
        public string WeaponBlueprintName { get; set; }

        [Ignore]
        public BelongsTo<ShipEntity> AttachedTo =>
            BelongsTo<ShipEntity>("AttachedTo", () => ShipId, id => ShipId = id);
    }

    public interface IGunRackRepository : ISavableRepository<GunRackEntity>
    {
        public static GunRackEntity LoadForShip(ShipEntity ship) =>
            Table.FirstOrDefault(s => s.ShipId == ship.ID);
    }
}