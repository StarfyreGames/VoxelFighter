using System.Collections.Generic;
using UnityEngine;

namespace Ship.Model
{
    [CreateAssetMenu]
    public class ShipAssetCatalogue : ScriptableObject
    {
        [SerializeField] public List<GameObject> shipPrefabs;
        
        public GameObject GetShipPrefab(string prefabName) =>
            shipPrefabs.Find(asset => asset.name == prefabName);
    }
}