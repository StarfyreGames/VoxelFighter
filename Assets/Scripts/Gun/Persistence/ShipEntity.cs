using System.Linq;
using Persistence;
using Persistence.Savable;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Gun.Persistence
{
    public class ShipEntity : Savable<ShipEntity>
    {
        public string Name { get; set; }

        // === Relationships

        [Ignore]
        public HasOne<GunRackEntity> GunRack => HasOne(
            "GunRack",
            () => IGunRackRepository.LoadForShip(this),
            gunRack => gunRack.AttachedTo.Set(this)
        );
    }

    public interface IShipRepository : ISavableRepository<ShipEntity>
    {
        public const int PlayerId = 1;

        public static ShipEntity GetPlayerShip() => GetById(PlayerId);
    }
}