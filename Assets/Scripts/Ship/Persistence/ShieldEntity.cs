using System.Collections.Generic;
using System.Linq;
using Persistence.Savable;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Ship.Persistence
{
    public class ShieldEntity : Savable<ShieldEntity>
    {
        public int Shield { get; set; }
        public int MaxShield { get; set; }

        public int Order { get; set; }

        // TODO nic: Do we want a prefab name to show the shield on screen?

        public int ShipId { get; set; }

        // === Relationships

        [Ignore]
        public BelongsTo<ShipEntity> Ship => BelongsTo<ShipEntity>(
            "Ship",
            () => ShipId,
            id => ShipId = id
        );
    }

    public interface IShieldRepository : ISavableRepository<ShieldEntity>
    {
        public static List<ShieldEntity> GetForShip(ShipEntity ship) =>
            Table
                .Where(entity => entity.ShipId == ship.ID)
                .OrderBy(entity => entity.Order)
                .ToList();
    }
}