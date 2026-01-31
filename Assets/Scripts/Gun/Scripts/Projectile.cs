using System;
using System.Linq;
using Gun.Api;
using Gun.Model;
using UnityEngine;
using utils;

namespace Gun.Scripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] public Bullet bullet;

        private int _impactsRemaining;

        private float TrueDamage =>
            (float)_impactsRemaining / bullet.maxPassthroughImpacts *
            bullet.passthroughDamageReductionFactor * bullet.damage;

        private Vector3 CurrentVelocity =>
            // TODO nic: I have no idea if this is correct - going to run with is and check later
            (float)_impactsRemaining / bullet.maxPassthroughImpacts *
            bullet.passthroughFrictionFactor * bullet.velocity * transform.forward;
        
        private void Awake()
        {
            _impactsRemaining = bullet.maxPassthroughImpacts;
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
            var movement = bullet.velocity * Time.deltaTime;
            transform.Translate(movement * transform.forward);

            // Adding half the bullets length to make sure the whole bullet
            // is accounted for - we measure from the center of the bullet
            var hitDistance = movement + transform.localScale.z * 0.5f;
            var raw = new Ray(transform.position, transform.forward);

            // Get all the things that we are going to hit in this frame
            // Allocating 10 slots - because why would it be more than that... Right?
            var hits = new RaycastHit[10];
            var hitCnt = Physics.RaycastNonAlloc(raw, hits, hitDistance);
            // We want to hit thing in order from closes to furthest - returned order is not guaranteed
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            for (var i = 0; i < hitCnt; i++)
            {
                // Send the damage to what we just hit
                var damageHandlers = hits[i].collider.gameObject.GetComponents<IShootable>();

                // If the thing we hit cannot be damaged - skip it
                if (damageHandlers.Length == 0) continue;

                // Otherwise give 'em hell
                foreach (var target in damageHandlers) target.TakeDamage(TrueDamage);

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
            // TODO nic: Do I need to clean up the bullet?
            Destroy(gameObject, 1f);
            Destroy(this);
        }
    }
}