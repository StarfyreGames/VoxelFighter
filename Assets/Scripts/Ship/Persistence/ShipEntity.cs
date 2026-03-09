using Gun.Persistence;
using Persistence.Savable;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Ship.Persistence
{
    public class ShipEntity : Savable<ShipEntity>
    {
        public string Name { get; set; }

        public int Speed { get; set; }
        
        // === Relationships

        [Ignore]
        public HasOne<GunRackEntity> GunRack => HasOne(
            "GunRack",
            () => IGunRackRepository.LoadForShip(this),
            gunRack => gunRack.AttachedTo.Set(this)
        );

        [Ignore]
        public HasOne<HealthEntity> Health => HasOne(
            "Health",
            () => IHeathRepository.GetForShip(this),
            entity => entity.Ship.Set(this)
        );

        [Ignore]
        public HasMany<ShieldEntity> Shields => HasMany(
            "Shields",
            () => IShieldRepository.GetForShip(this),
            entity => entity.Ship.Set(this)
        );
        
        // === Computed properties
        
        [Ignore] public bool HasShields => Shields.Resolve().Count > 0;
    }

    public interface IShipRepository : ISavableRepository<ShipEntity>
    {
        public const int PlayerShipId = 1;

        public static ShipEntity GetPlayerShip() => GetById(PlayerShipId);
    }
}