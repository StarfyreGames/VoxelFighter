namespace Guns.Clip
{
    public class SingleShotClip : AClip
    {
        public override void Fire(GunSpec gunSpec, BulletSpec bulletSpec)
        {
            BuildBullet(gunSpec, bulletSpec);
        }
    }
}