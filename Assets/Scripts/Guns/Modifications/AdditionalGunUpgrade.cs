namespace Guns.Modifications
{
    public class AdditionalGunUpgrade : AModification
    {
        public override void Modify(Armament armament)
        {
            armament.AddGun();
        }
    }
}