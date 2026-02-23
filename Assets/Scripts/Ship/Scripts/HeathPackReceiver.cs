using System.Linq;
using PowerUp.Api;
using Ship.Api;
using Ship.Persistence;
using UnityEngine;

namespace Ship.Scripts
{
    public class HeathPackReceiver : MonoBehaviour, IHeathPackReceiver
    {
        private ShipEntity _ship;

        private void Awake()
        {
            _ship = GetComponent<IShipResolver>().GetShip();
        }

        public void AcceptHeathPack(int recoverAmount)
        {
            if (_ship == null)
                return;

            var health = _ship.Health.Resolve();
            var injury = health.MaxHealth - health.Health;

            health.Health += recoverAmount;
            recoverAmount -= injury;

            if (recoverAmount <= 0)
                return;
            
            // TODO nic: Confirm that health pack also heals the shield
            var shields = _ship.Shields.Resolve()
                .Where(shield => shield.MaxShield != shield.Shield)
                .ToList();
            
            foreach (var shield in shields)
            {
                if (shield.Shield + recoverAmount <= shield.MaxShield)
                {
                    shield.Shield += recoverAmount;
                    return;
                }

                var diff = shield.MaxShield - shield.Shield;
                shield.Shield = shield.MaxShield;
                recoverAmount -= diff;
            }
        }
    }
}