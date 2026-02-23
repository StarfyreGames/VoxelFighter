using System.Linq;
using PowerUp.Api;
using Ship.Api;
using Ship.Persistence;
using UnityEngine;

namespace Ship.Scripts
{
    public class ShieldUpgradeReceiver : MonoBehaviour, IShieldUpgradeReceiver
    {
        private ShipEntity _ship;

        private void Awake()
        {
            _ship = GetComponent<IShipResolver>().GetShip();
        }

        public void AcceptNewShield(ShieldEntity shield)
        {
            shield.Order = _ship.Shields.Resolve().Count;
            _ship.Shields.Add(shield);
        }
    }
}