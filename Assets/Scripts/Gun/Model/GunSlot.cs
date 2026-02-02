using System;
using Gun.Scripts;
using JetBrains.Annotations;
using UnityEngine;

namespace Gun.Model
{
    public class GunSlot : ScriptableObject
    {
        [SerializeField] public Transform transform;
        [SerializeField] [CanBeNull] public GameObject weaponPrefab;

        public RuntimeGunSlot ToRuntimeGunSlot()
        {
            return new RuntimeGunSlot
            {
                Transform = transform,
                WeaponPrefab = weaponPrefab,
                Weapon = weaponPrefab?.GetComponent<Weapon>()
            };
        }
    }

    public record RuntimeGunSlot
    {
        public static RuntimeGunSlot FromGunSlot(GunSlot gunSlot)
        {
            return new RuntimeGunSlot
            {
                Transform = gunSlot.transform,
                WeaponPrefab = gunSlot.weaponPrefab,
                Weapon = gunSlot.weaponPrefab?.GetComponent<Weapon>()
            };
        }

        public Transform Transform;
        [CanBeNull] public GameObject WeaponPrefab;
        [CanBeNull] public Weapon Weapon;

        public void FireWeapon()
        {
            if (Weapon != null) Weapon.Fire();
        }

        public RuntimeGunSlot ChangeWeapon(GameObject weaponPrefab)
        {
            return this with
            {
                WeaponPrefab = weaponPrefab,
                Weapon = weaponPrefab?.GetComponent<Weapon>()
            };
        }
    }
}