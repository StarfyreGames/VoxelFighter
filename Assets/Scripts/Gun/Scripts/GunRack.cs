using System.Collections.Generic;
using Gun.Model;
using Gun.Persistence;
using UnityEngine;

namespace Gun.Scripts
{
    public class GunRack : MonoBehaviour
    {
        [SerializeField] [Tooltip("All the points on the host that can accept a gun")]
        public List<Transform> gunSlots;

        public GunAssetCatalogue catalogue;

        private GameObject[] _rack;
        private Weapon[] _weapons;

        public void Fire()
        {
            foreach (var weapon in _weapons)
                weapon?.Fire();
        }

        private void Awake()
        {
            _rack = new GameObject[gunSlots.Count];
            _weapons = new Weapon[gunSlots.Count];
        }

        private void Start()
        {
            // TODO nic: This should not be specific to the player
            var layout = IShipRepository.GetPlayerShip().GunRack.Resolve();

            // TODO nic: Better error handling?
            foreach (var save in layout.Guns.Resolve())
            {
                // Can't create a gun slot that doesn't exist.
                if (save.Slot < 0 || save.Slot > _rack.Length) return;

                // Slots are numbered 1 - n
                var gunSlotIndex = save.Slot - 1;

                // Clean up any guns that are already loaded into the scene
                if (_rack[gunSlotIndex] != null)
                    Destroy(_rack[gunSlotIndex]);

                // Create the new gun in the schene
                var slotTransform = gunSlots[gunSlotIndex];
                var gunPrefab = catalogue.GetWeaponPrefab(save.WeaponPrefab);
                var gun = Instantiate(gunPrefab, slotTransform);

                // Initialise the weapon's information into runtime
                var weapon = gun.GetComponent<Weapon>();
                weapon.Initialise(save.FireMode.Resolve(), save.Bullet.Resolve(), catalogue);

                // Update the rack and return the new game object
                _rack[gunSlotIndex] = gun;
                _weapons[gunSlotIndex] = weapon;
            }
        }
    }
}