using System;
using Gun.Model;
using PowerUp.Api;
using UnityEngine;

namespace PowerUp.Scripts
{
    public class ChangeGunPowerUpScript : MonoBehaviour
    {
        public WeaponBlueprint blueprint;

        private void OnTriggerEnter(Collider other)
        {
            var gunUpgradeReceiver = other.gameObject.GetComponentInParent<IGunUpgradeReceiver>();
            if (gunUpgradeReceiver == null) return;
            
            gunUpgradeReceiver.ChangeGun(blueprint);
            Destroy(gameObject);
        }
    }
}