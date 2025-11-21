using Guns.Clip;
using UnityEngine;

namespace Guns.Modifications
{
    public abstract class AModification : MonoBehaviour
    {
        public virtual GunSpec Modify(GunSpec spec)
        {
            return spec;
        }

        public virtual BulletSpec Modify(BulletSpec spec)
        {
            return spec;
        }

        public virtual AClip Modify(AClip clip)
        {
            return clip;
        }

        public virtual void Modify(Armament armament)
        {
            // Default intentionally left blank
        }
    }

    public interface IModifiable
    {
        public void ApplyModification(AModification modification);
    }
}