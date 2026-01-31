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
        [SerializeField] public GameObject projectilePrefab;
        [SerializeField] public FireMode fireMode;
        [SerializeField] public Bullet bullet;

        private float _lastFired;

        private void Fire()
        {
            // Don't shoot too quickly
            if (Time.fixedTime - _lastFired < fireMode.fireRate) return;
            _lastFired = Time.fixedTime;
            
            // Get the bullets into the scene and attach the relevant information
            foreach (
                var bulletGo in BuildBulletTransforms(fireMode.numberOfBullets)
                    .Select(bulletTransform => Instantiate(projectilePrefab, bulletTransform.position, bulletTransform.rotation))
            )
            {
                bulletGo.transform.localScale = Vector3.one * bullet.scale;
                bulletGo.GetComponent<Projectile>().bullet = bullet;
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
                var degrees = Mathf.Lerp(-fireMode.spreadAngle / 2f, fireMode.spreadAngle / 2f, t);
                var spreadRotation = Quaternion.AngleAxis(degrees, muzzle.forward);

                bulletTransforms[i] = Instantiate(muzzle, muzzle.position, spreadRotation);
            }

            return bulletTransforms;
        }

        private void Start()
        {
            _lastFired = Time.fixedTime;
        }

        private void Update()
        {
            if (fireMode.autoFire) Fire();
        }
    }
}