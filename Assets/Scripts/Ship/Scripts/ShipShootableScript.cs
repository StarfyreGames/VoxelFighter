using System;
using System.Linq;
using Gun.Api;
using Ship.Api;
using Ship.Persistence;
using UnityEngine;

namespace Ship.Scripts
{
    public class ShipShootableScript : MonoBehaviour, IShootable
    {
        private ShipEntity _ship;
        
        private void Awake()
        {
            _ship = GetComponent<IShipResolver>().GetShip();
        }

        public void TakeDamage(int damage)
        {
            if (_ship == null) 
                return;

            // See if the shield can help you
            var remaining = HandleShieldStrike(damage);

            // If not - take the damage
            if (remaining > 0)
                HandleHeathLost(remaining);
        }

        // Returns the damage left after the shields have taken what they can
        private int HandleShieldStrike(int damage)
        {
            // There are no shields - lol
            if (!_ship.HasShields)
                return damage;

            // We only need the shields that aren't already depleted
            var shields = _ship.Shields.Resolve()
                .Where(shield => shield.Shield > 0)
                .ToList();

            // If there aren't any - lol
            if (shields.Count == 0)
                return damage;
            
            foreach (var shield in shields)
            {
                // If a shield can take the damage, take it and we are done
                if (shield.Shield > damage)
                {
                    shield.Shield -= damage;
                    return 0;
                }

                // Otherwise take the shield to 0 and update damage to what's left
                var remaining = damage - shield.Shield;
                shield.Shield = 0;
                damage = remaining;
            }

            // What ever is left wasn't absorbed by the shield(s)
            return damage;
        }

        private void HandleHeathLost(int damage)
        {
            // Health can't go below 0
            var health = _ship.Health.Resolve();
            health.Health = Mathf.Max(0, health.Health - damage);

            // Game over for this ship
            if (health.Health == 0)
            {
                // TODO nic: What happens when you die
            }
        }
    }
}