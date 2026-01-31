using UnityEngine;

namespace Gun.Model
{
    [CreateAssetMenu]
    public class FireMode : ScriptableObject
    {
        public float fireRate;

        public bool autoFire;

        [Min(1)]
        public int numberOfBullets = 1;

        [Range(0, 360)] public int spreadAngle;
    }
}