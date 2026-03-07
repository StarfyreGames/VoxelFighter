using System.Collections.Generic;
using Gun.Persistence;
using JetBrains.Annotations;
using UnityEngine;

namespace Gun.Model
{
    [CreateAssetMenu]
    public class WeaponBlueprint : ScriptableObject
    {
        public string weaponName;
        
        [Range(100f, 1000f)]
        public float bulletVelocity;
        
        [Min(0f)]
        public int bulletDamage;
        
        [Range(0f, 10f)]
        public float bulletScale = 1;
        
        [Min(1)]
        public int numberOfBullets = 1;
        
        public float fireRate;

        public List<int> gunLocations;
        
        [Range(0f, 360f)]
        public float spreadAngle;
        
        public GameObject bulletPrefab;
        
        public GameObject weaponPrefab;
        
        [CanBeNull] public WeaponBlueprint nextUpgrade;
    }
}