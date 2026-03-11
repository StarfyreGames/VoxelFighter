using System.Linq;
using Gun.Model;
using PowerUp.Api;
using UnityEngine;

namespace Gun.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] public Transform muzzle;


        private WeaponBlueprint _blueprint;
        private bool _initialized;

        private float _lastFired;

        public void Initialise(WeaponBlueprint blueprint)
        {
            _blueprint = blueprint;
            _initialized = true;
        }

        public void Fire()
        {
            if (!_initialized) return;

            // Don't shoot too quickly
            if (Time.fixedTime - _lastFired < _blueprint.fireRate) return;
            _lastFired = Time.fixedTime;

            // Get the bullets into the scene and attach the relevant information

            //*** THIS SECTION seems to be causing a null reference issue - see console output in game ***\\
            var firedBullets = BuildBulletTransforms(_blueprint.numberOfBullets)
                .Select(bulletTransform =>
                    Instantiate(
                        _blueprint.bulletPrefab,
                        bulletTransform.position,
                        bulletTransform.rotation
                    )
                );

            foreach (var bulletGo in firedBullets)
            {
                bulletGo.transform.localScale = Vector3.one * _blueprint.bulletScale;
                bulletGo.GetComponent<Projectile>().Initialise(_blueprint);
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
                var degrees = Mathf.Lerp(-_blueprint.spreadAngle / 2f, _blueprint.spreadAngle / 2f, t);
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