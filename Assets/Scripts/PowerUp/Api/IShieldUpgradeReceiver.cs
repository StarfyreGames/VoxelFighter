using Ship.Persistence;

namespace PowerUp.Api
{
    public interface IShieldUpgradeReceiver
    {
        void AcceptNewShield(ShieldEntity shield);
        
    }
}