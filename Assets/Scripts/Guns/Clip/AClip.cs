using JetBrains.Annotations;
using UnityEngine;

namespace Guns.Clip
{
    public abstract class AClip
    {
        public abstract void Fire(GunSpec gunSpec, BulletSpec bulletSpec);

        public virtual void Update(GunSpec gunSpec, BulletSpec bulletSpec, float deltaTime)
        {
            // Left blank in the default
        }

        [CanBeNull]
        protected GameObject BuildBullet(GunSpec gunSpec, BulletSpec bulletSpec)
        {
            
            var position = gunSpec.Position;
            var bullet = Object.Instantiate(gunSpec.bulletPrefab, position, Quaternion.Euler(90f, 0f, 0f));
            var projectile = bullet.GetComponent<Projectile>();

            if (projectile == null)
            {
                Object.Destroy(bullet);
                return null;
            }

            projectile.Initialise(bulletSpec, bullet);

            return bullet;
        }
    }
}