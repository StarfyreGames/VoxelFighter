using UnityEngine;

namespace Guns.Modifications
{
    public class FireRateMultiplierUpgrade : AModification
    {
        [SerializeField] public float multiplier = 2f;
        
        public override GunSpec Modify(GunSpec spec)
        {
            return spec with { fireRate = spec.fireRate * multiplier };
        }
    }
}