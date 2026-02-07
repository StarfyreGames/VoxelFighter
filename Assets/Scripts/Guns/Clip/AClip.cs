using JetBrains.Annotations;
using UnityEngine;

namespace Guns.Clip
{
    public abstract class AClip
    {
        public abstract void Fire(GunSpec gunSpec, BulletEntitySpec bulletEntitySpec);

        public virtual void Update(GunSpec gunSpec, BulletEntitySpec bulletEntitySpec, float deltaTime)
        {
            // Left blank in the default
        }

        [CanBeNull]
        protected GameObject BuildBullet(GunSpec gunSpec, BulletEntitySpec bulletEntitySpec)
        {
            
            var position = gunSpec.Position;
            var bullet = Object.Instantiate(gunSpec.bulletPrefab, position, Quaternion.Euler(90f, 0f, 0f));
            var projectile = bullet.GetComponent<Projectile>();

            if (projectile == null)
            {
                Object.Destroy(bullet);
                return null;
            }

            projectile.Initialise(bulletEntitySpec, bullet);

            return bullet;
        }
    }
}