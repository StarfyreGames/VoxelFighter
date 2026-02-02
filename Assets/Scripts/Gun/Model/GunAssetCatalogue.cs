using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Gun.Model
{
    [CreateAssetMenu]
    public class GunAssetCatalogue : ScriptableObject
    {
        [SerializeField] public List<Bullet> bullets;
        [SerializeField] public List<FireMode> fireModes;
        [SerializeField] public List<GameObject> guns;

        [CanBeNull]
        public Bullet GetBulletByName(string assetName)
        {
            return bullets.Find(asset => asset.name == assetName);
        }

        [CanBeNull]
        public string GetBulletName(Bullet bullet)
        {
            // Just to make sure that we don't work with a bad asset name
            return GetBulletByName(bullet.name)?.name;
        }

        [CanBeNull]
        public FireMode GetFireModeByName(string assetName)
        {
            return fireModes.Find(asset => asset.name == assetName);
        }

        [CanBeNull]
        public string GetFireModeName(FireMode fireMode)
        {
            // Just to make sure that we don't work with a bad asset name
            return GetFireModeByName(fireMode.name)?.name;
        }

        [CanBeNull]
        public GameObject GetGunByName(string assetName)
        {
            return guns.Find(asset => asset.name == assetName);
        }

        [CanBeNull]
        public string GetGunName(GameObject gun)
        {
            // Just to make sure that we don't work with a bad asset name
            return GetGunByName(gun.name)?.name;
        }
    }
}