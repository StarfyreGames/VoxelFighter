using System.Linq;
using Gun.Model;
using Gun.Persistence;
using UnityEngine;

namespace Gun.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] public Transform muzzle;

        private GunAssetCatalogue _catalogue;
        private FireModeEntity _fireModeEntity;
        private BulletEntity _bulletEntity;
        private bool _initialized;

        private float _lastFired;

        public void Initialise(FireModeEntity filterModeEntity, BulletEntity bulletEntity, GunAssetCatalogue catalogue)
        {
            _catalogue = catalogue;
            _fireModeEntity = filterModeEntity;
            _bulletEntity = bulletEntity;
            _initialized = true;
        }

        public void Fire()
        {
            if (!_initialized) return;

            // Don't shoot too quickly
            if (Time.fixedTime - _lastFired < _fireModeEntity.FireRate) return;
            _lastFired = Time.fixedTime;

            // Get the bullets into the scene and attach the relevant information
            var firedBullets = BuildBulletTransforms(_fireModeEntity.NumberOfBullets)
                .Select(bulletTransform =>
                    Instantiate(
                        _catalogue.GetBulletPrefab(_bulletEntity),
                        bulletTransform.position,
                        bulletTransform.rotation
                    )
                );

            foreach (var bulletGo in firedBullets)
            {
                bulletGo.transform.localScale = Vector3.one * _bulletEntity.Scale;
                bulletGo.GetComponent<Projectile>().Initialise(_bulletEntity);
            }
        }

        private Transform[] BuildBulletTransforms(int count)
        {
            // If there is only one bullet just early out - there is no need to do maths
            if (count == 1) return new[] { muzzle };

            // Take the original muzzle transform and move it to
            // point in the direction we would like the bullet to travel
            var bulletTransforms = new Transform[count];
            for (var i = 0; i < count; i++)
            {
                var t = (float)i / (count - 1);
                var degrees = Mathf.Lerp(-_fireModeEntity.SpreadAngle / 2f, _fireModeEntity.SpreadAngle / 2f, t);
                var spreadRotation = Quaternion.AngleAxis(degrees, muzzle.forward);

                bulletTransforms[i] = Instantiate(muzzle, muzzle.position, spreadRotation);
            }

            return bulletTransforms;
        }

        private void Start()
        {
            if (!_initialized) return;
            _lastFired = Time.fixedTime;
        }
    }
}