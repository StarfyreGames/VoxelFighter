using Guns.Clip;

namespace Guns.Modifications
{
    public class BurstFireUpgrade : AModification
    {
        public int shotCount;
        public float spreadAngle;
        
        public override AClip Modify(AClip clip)
        {
            return new BurstShotClip(shotCount, spreadAngle);
        }
    }
}