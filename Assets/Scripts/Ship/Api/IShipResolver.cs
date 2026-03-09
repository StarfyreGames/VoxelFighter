using Ship.Persistence;

namespace Ship.Api
{
    public interface IShipResolver
    {
        public ShipEntity GetShip();
    }
}