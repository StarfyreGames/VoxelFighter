using System.Linq;
using Persistence.Savable;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Ship.Persistence
{
    public class HealthEntity : Savable<HealthEntity>
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        [Indexed] public int ShipId { get; set; }

        // === Relationships

        [Ignore]
        public BelongsTo<ShipEntity> Ship => BelongsTo<ShipEntity>(
            "Ship",
            () => ShipId,
            id => ShipId = id
        );
    }

    public interface IHeathRepository : ISavableRepository<HealthEntity>
    {
        public static HealthEntity GetForShip(ShipEntity ship) =>
            Table.FirstOrDefault(entity => entity.ShipId == ship.ID);
    }
}