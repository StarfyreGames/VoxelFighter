using Persistence.Savable;

namespace Ship.Persistence
{
    public class AutonomousShipEntity : Savable<AutonomousShipEntity>
    {
        public string PrefabName { get; set; }
        public string RouteName { get; set; }

        public int ShipId { get; set; }

        // === Relationships

        public HasOne<ShipEntity> Ship => HasOne(
            "Ship",
            () => IShipRepository.GetById(ShipId)
        );
    }
}