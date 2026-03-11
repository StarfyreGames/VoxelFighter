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

            if (gunUpgradeReceiver == null)
            { 
                Debug.Log("Can't Find reciever");
                return; 
            }

            gunUpgradeReceiver.UpgradeGun();
            Destroy(gameObject);
        }
    }
}