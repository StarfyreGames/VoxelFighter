using System;
using PowerUp.Api;
using UnityEngine;

namespace PowerUp.Scripts
{
    public class UpgradeWeaponPowerUpScript : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var gunUpgradeReceiver = other.gameObject.GetComponentInParent<IGunUpgradeReceiver>();

            if (gunUpgradeReceiver == null) return;

            gunUpgradeReceiver.UpgradeGun();
            Destroy(gameObject);
        }
    }
}