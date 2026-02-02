using System;
using System.Collections.Generic;
using System.Linq;
using Gun.Model;
using Unity.VisualScripting;
using UnityEngine;

namespace Gun.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] public Transform muzzle;

        private FireMode _fireMode;
        private Bullet _bullet;
        private bool _initialized;

        private float _lastFired;

        public void Init(FireMode filterMode, Bullet bullet)
        {
            _fireMode = filterMode;
            _bullet = bullet;
            _initialized = true;
        }

        public void Fire()
        {
            if (!_initialized) return;

            // Don't shoot too quickly
            if (Time.fixedTime - _lastFired < _fireMode.fireRate) return;
            _lastFired = Time.fixedTime;

            // Get the bullets into the scene and attach the relevant information
            var firedBullets = BuildBulletTransforms(_fireMode.numberOfBullets)
                .Select(bulletTransform =>
                    Instantiate(_bullet.projectilePrefab, bulletTransform.position, bulletTransform.rotation)
                );

            foreach (var bulletGo in firedBullets)
            {
                bulletGo.transform.localScale = Vector3.one * _bullet.scale;
                bulletGo.GetComponent<Projectile>().bullet = _bullet;
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
                var degrees = Mathf.Lerp(-_fireMode.spreadAngle / 2f, _fireMode.spreadAngle / 2f, t);
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