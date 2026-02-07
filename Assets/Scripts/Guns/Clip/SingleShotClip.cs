namespace Guns.Clip
{
    public class SingleShotClip : AClip
    {
        public override void Fire(GunSpec gunSpec, BulletEntitySpec bulletEntitySpec)
        {
            BuildBullet(gunSpec, bulletEntitySpec);
        }
    }
}