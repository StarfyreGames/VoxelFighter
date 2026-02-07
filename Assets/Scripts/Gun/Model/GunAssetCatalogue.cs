using System.Collections.Generic;
using Gun.Persistence;
using UnityEngine;

namespace Gun.Model
{
    [CreateAssetMenu]
    public class GunAssetCatalogue : ScriptableObject
    {
        [SerializeField] public List<GameObject> weaponPrefabs;
        [SerializeField] public List<GameObject> bulletPrefabs;

        public GameObject GetBulletPrefab(BulletEntity bulletEntity) =>
            bulletPrefabs.Find(prefab => prefab.name == bulletEntity.PrefabName);

        public GameObject GetWeaponPrefab(string prefabName) =>
            weaponPrefabs.Find(asset => asset.name == prefabName);
    }
}