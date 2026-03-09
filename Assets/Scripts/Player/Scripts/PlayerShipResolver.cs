using System;
using Gun.Api;
using Gun.Persistence;
using Ship.Api;
using Ship.Persistence;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerShipResolver : MonoBehaviour, IShipResolver
    {
        private ShipEntity _ship;

        private void Awake() => _ship = IShipRepository.GetPlayerShip();

        public ShipEntity GetShip() => _ship;
    }
}