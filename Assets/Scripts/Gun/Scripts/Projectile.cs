using System;
using System.Linq;
using Gun.Api;
using Gun.Model;
using Gun.Persistence;
using UnityEngine;
using utils;

namespace Gun.Scripts
{
    public class Projectile : MonoBehaviour
    {
        private WeaponBlueprint _blueprint;
        private int _impactsRemaining;
      
        public void Initialise(WeaponBlueprint blueprint)
        {
            _blueprint = blueprint;
        }
              

        private void Update()
        {
            // Clean up if the bullet has left the play area
            if (CleanUpFence.ShouldDestroy(gameObject))
            {
                DestroyProjectile();
                return;
            }

            // Work out how far we would move in this frame.
            var movement = _blueprint.bulletVelocity * Time.deltaTime;
            transform.Translate(movement * transform.forward);

            // Adding half the bullets length to make sure the whole bullet
            // is accounted for - we measure from the center of the bullet
            var hitDistance = movement;
            var ray = new Ray(transform.position, transform.forward);

            // Get all the things that we are going to hit in this frame
            // Allocating 10 slots - because why would it be more than that... Right?
            var hitList = new RaycastHit[10];
            var hitCnt = Physics.RaycastNonAlloc(ray, hitList, hitDistance);

            // We want to hit thing in order from closes to furthest - returned order is not guaranteed
            var hits = hitList.ToList().Take(hitCnt).ToList();
            hits.Sort((a, b) => a.distance.CompareTo(b.distance));

            for (var i = 0; i < hitCnt; i++)
            {
                // Send the damage to what we just hit
                var damageHandlers = hits[i].collider.gameObject.GetComponentsInParent<IShootable>();

                // If the thing we hit cannot be damaged - skip it
                if (damageHandlers.Length == 0) continue;

                // Otherwise give 'em hell
                foreach (var target in damageHandlers) target.TakeDamage(_blueprint.bulletDamage);

                // Record the impact and keep going if the bullet is still moving
                --_impactsRemaining;
                if (_impactsRemaining > 0) continue;

                // Clean up if not
                DestroyProjectile();
                return;
            }
        }


        private void DestroyProjectile()
        {
            Destroy(gameObject);            
        }
    }
}