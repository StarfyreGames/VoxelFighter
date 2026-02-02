using UnityEngine;

namespace Gun.Model
{
    [CreateAssetMenu]
    public class Bullet : ScriptableObject
    {
        [Range(50f, 500f)]
        public float velocity;
        
        [Range(0f, 100f)]
        public float damage;

        [Range(1f, 10f)]
        public float scale = 1;
        
        public int maxPassthroughImpacts = 1;
        
        [Range(0f, 1f)]
        public float passthroughDamageReductionFactor;

        [Range(0f, 1f)]
        public float passthroughFrictionFactor;
        
        public GameObject projectilePrefab;
    }
}