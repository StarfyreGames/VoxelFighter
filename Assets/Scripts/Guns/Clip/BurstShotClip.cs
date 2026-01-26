using System.Collections.Generic;
using UnityEngine;

namespace Guns.Clip
{
    public class BurstShotClip : AClip
    {
        private readonly int _shotCount;
        private readonly float _spreadAngle;

        public BurstShotClip(int shotCount, float spreadAngle)
        {
            _shotCount = shotCount;
            _spreadAngle = spreadAngle;
        }

        public override void Fire(GunSpec gunSpec, BulletSpec baseBulletSpec)
        {
            foreach (var bulletSpec in GenerateBulletSpecs(baseBulletSpec))
            {
                BuildBullet(gunSpec, bulletSpec);
            }
        }

        private List<BulletSpec> GenerateBulletSpecs(BulletSpec baseBulletSpec)
        {
            if (_shotCount <= 1)
                return new List<BulletSpec> { baseBulletSpec };

            var bulletSpecs = new List<BulletSpec>();

            for (var i = 0; i < _shotCount; i++)
            {
                var t = (float)i / (_shotCount - 1);
                var degrees = Mathf.Lerp(-_spreadAngle / 2f, _spreadAngle / 2f, t);
                var spreadRotation = Quaternion.AngleAxis(degrees, Vector3.up);

                bulletSpecs.Add(baseBulletSpec with { velocity = spreadRotation * baseBulletSpec.velocity });
            }

            return bulletSpecs;
        }
    }
}