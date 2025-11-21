using System;
using Guns.Clip;
using Guns.Modifications;
using UnityEngine;

namespace Guns
{
    public class Gun : IModifiable
    {
        private readonly Specs _specs;
        private DateTime _lastFire;

        public Gun(Specs specs)
        {
            _specs = specs;
        }

        public void Fire()
        {
            Debug.Log("'+++++' " + _specs.GunSpec.fireRate);
            var duration = TimeSpan.FromSeconds(1 / _specs.GunSpec.fireRate);
            var timeElapsed = DateTime.UtcNow - _lastFire;
            if (timeElapsed < duration) return;

            _specs.Clip.Fire(_specs.GunSpec, _specs.BulletSpec);
            _lastFire = DateTime.UtcNow;
        }

        public void Update(float deltaTime)
        {
            _specs.Clip.Update(_specs.GunSpec, _specs.BulletSpec, deltaTime);
        }

        public void ApplyModification(AModification modification)
        {
            Debug.Log("'+++++ before' " + _specs.GunSpec.fireRate);
            _specs.ApplyModification(modification);
            Debug.Log("'+++++ after' " + _specs.GunSpec.fireRate);
        }

        public void DrawGizmo()
        {
            Gizmos.color = Color.darkRed;

            var size = Vector3.one * 2;
            size.z *= 4;

            Gizmos.DrawCube(_specs.GunSpec.Position, size);
        }
    }
}