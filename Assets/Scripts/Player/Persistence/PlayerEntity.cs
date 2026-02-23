using Gun.Persistence;
using Persistence.Savable;
using Ship.Persistence;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Player.Persistence
{
    public class PlayerEntity : Savable<PlayerEntity>
    {
        public const int PlayerID = 1;

        [PrimaryKey]
        public override int ID
        {
            get => PlayerID;
            set { /* NOP - this is fixed */ }
        }
        
        public string Name { get; set; }
        
        // === Relationships

        [Ignore]
        public HasOne<ShipEntity> Ship => HasOne(
            "Ship",
            () => IShipRepository.GetById(PlayerID)
        );
    }
}