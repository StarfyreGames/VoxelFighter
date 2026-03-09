using Gun.Model;

namespace PowerUp.Api
{
    public interface IGunUpgradeReceiver
    {
        public void UpgradeGun();
        public void ChangeGun(WeaponBlueprint blueprint);
    }
}