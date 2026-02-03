using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks.Triggers;
using Gun.Model;
using Gun.Persistence;
using JetBrains.Annotations;
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
            var layout = GunLayoutSave.LoadForPlayer();

            // TODO nic: Better error handling?
            foreach (var gunSlotSave in layout.Guns())
            {
                // Can't create a gun slot that doesn't exist.
                if (gunSlotSave.Slot < 0 || gunSlotSave.Slot > _rack.Length) return;

                LoadGunIntoScene(
                    gunSlotSave.Slot,
                    catalogue.GetGunByName(gunSlotSave.GunAssetName),
                    catalogue.GetFireModeByName(gunSlotSave.FireModeAssetName),
                    catalogue.GetBulletByName(gunSlotSave.BulletAssetName)
                );
            }
        }

        private void LoadGunIntoScene(int slot, GameObject gunPrefab, FireMode fireMode, Bullet bullet)
        {
            // Slots are numbered 1 - n
            var gunSlotIndex = slot - 1;

            // Clean up any guns that are already loaded into the scene
            if (_rack[gunSlotIndex] != null)
                Destroy(_rack[gunSlotIndex]);

            // Create the new gun in the schene
            var slotTransform = gunSlots[gunSlotIndex];
            var gun = Instantiate(gunPrefab, slotTransform);

            // Initialise the weapon's information into runtime
            var weapon = gun.GetComponent<Weapon>();
            weapon.Init(fireMode, bullet);

            // Update the rack and return the new game object
            _rack[gunSlotIndex] = gun;
            _weapons[gunSlotIndex] = weapon;
        }
    }
}