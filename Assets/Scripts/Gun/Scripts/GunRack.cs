using System.Collections.Generic;
using Gun.Api;
using Gun.Model;
using Gun.Persistence;
using PowerUp.Api;
using Ship.Api;
using UnityEngine;

namespace Gun.Scripts
{
    public class GunRack : MonoBehaviour, IGunUpgradeReceiver
    {
        [SerializeField] [Tooltip("All the points on the host that can accept a gun")]
        public List<Transform> gunSlots;

        public WeaponBlueprintCatalogue catalogue;

        private GameObject[] _rack;
        private Weapon[] _weapons;

        private GunRackEntity GunRackEntity =>
            GetComponent<IShipResolver>()?.GetShip().GunRack.Resolve();

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
            var layout = GunRackEntity;
            if (layout == null) return;

            BuildGameObjects(catalogue.FindByName(layout.WeaponBlueprintName));
        }

        public void UpgradeGun()
        {
            var layout = GunRackEntity;
            if (layout == null) return;

            var weaponBlueprint = catalogue.FindByName(layout.WeaponBlueprintName);
            if (weaponBlueprint.nextUpgrade == null) return;

            ChangeGun(weaponBlueprint.nextUpgrade);
        }

        public void ChangeGun(WeaponBlueprint blueprint)
        {
            var layout = GunRackEntity;
            if (layout == null) return;

            // Update in live object but this is temporary so no save
            // at this point so that it's reset on next fetch.
            layout.WeaponBlueprintName = blueprint.name;
            BuildGameObjects(blueprint);
        }

        private void BuildGameObjects(WeaponBlueprint weaponBlueprint)
        {
            // Clean up any guns that are already loaded into the scene
            foreach (var go in _rack)
                if (go != null)
                    Destroy(go);

            foreach (var slot in weaponBlueprint.gunLocations)
            {
                // Can't create a gun slot that doesn't exist.
                if (slot < 0 || slot > _rack.Length) return;

                // Slots are numbered 1 - n
                var gunSlotIndex = slot - 1;

                // Create the new gun in the schene
                var slotTransform = gunSlots[gunSlotIndex];
                var gun = Instantiate(weaponBlueprint.weaponPrefab, slotTransform);

                // Initialise the weapon's information into runtime
                var weapon = gun.GetComponent<Weapon>();
                weapon.Initialise(weaponBlueprint);

                // Update the rack and return the new game object
                _rack[gunSlotIndex] = gun;
                _weapons[gunSlotIndex] = weapon;
            }
        }
    }
}