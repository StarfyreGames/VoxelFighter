using System.Collections.Generic;
using System.Linq;
using Persistence;
using Persistence.Savable;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Gun.Persistence
{
    public class GunRackEntity : Savable<GunRackEntity>, IGunRackRepository
    {
        // === Relationships

        [Indexed(Unique = false)] public int ShipId { get; set; }

        [Ignore]
        public BelongsTo<ShipEntity> AttachedTo =>
            BelongsTo<ShipEntity>("AttachedTo", () => ShipId, id => ShipId = id);

        [Ignore]
        public HasMany<GunSlotEntity> Guns => HasMany(
            "Guns",
            () => IGunSlotRepository.GetForGunRack(this),
            slot => slot.AttachedTo.Set(this)
        );

        // === Utilities

        public void AddGun(GunSlotEntity gun) => Guns.Add(gun);
    }

    public interface IGunRackRepository : ISavableRepository<GunRackEntity>
    {
        public static GunRackEntity LoadForShip(ShipEntity ship) =>
            Table.FirstOrDefault(s => s.ShipId == ship.ID);
    }
}