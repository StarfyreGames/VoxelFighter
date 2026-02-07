using Guns.Clip;
using Guns.Modifications;
using UnityEngine;

namespace Guns
{
    public record Specs(GunSpec GunSpec, BulletEntitySpec BulletEntitySpec, AClip Clip) : IModifiable
    {
        private const float BaseGunFireRate = 3;
        
        private const int BaseBulletDamage = 10;
        private const float BaseBulletPower = 100;
        private static readonly Vector3 BaseBulletScale = new(2f, 2f, 2f);

        private static readonly AClip InitialClip = new SingleShotClip();
        
        public GunSpec GunSpec { get; private set; } = GunSpec;
        public BulletEntitySpec BulletEntitySpec { get; private set; } = BulletEntitySpec;
        public AClip Clip { get; private set; } = Clip;

        public Specs(GameObject host, Vector3 rotation, BulletEntitySpec.Origin origin, BulletCatalogue catalogue) : this(
            new GunSpec
            {
                fireRate = BaseGunFireRate,
                bulletPrefab = catalogue.basicBulletPrefab,
                offset = Vector3.zero,
                host = host,
            },
            new BulletEntitySpec
            {
                damage = BaseBulletDamage,
                scale = BaseBulletScale,
                velocity = rotation * BaseBulletPower,
                damageType = BulletEntitySpec.DamageType.Impact,
                origin = origin
            },
            InitialClip
        )
        {
        }

        public void ApplyModification(AModification modification)
        {
            GunSpec = modification.Modify(GunSpec);
            BulletEntitySpec = modification.Modify(BulletEntitySpec);
            Clip = modification.Modify(Clip);
        }

        public Specs CloneForGun(Vector3 offset)
        {
            return this with { GunSpec = GunSpec with { offset = offset } };
        }
    }

    public record GunSpec
    {
        public float fireRate;
        public Vector3 offset;
        public GameObject host;
        public GameObject bulletPrefab;

        public Vector3 Position => host.transform.position + offset;
    }

    public record BulletEntitySpec
    {
        public Vector3 scale;
        public Vector3 velocity;
        
        public int damage;
        public DamageType damageType;
        public Origin origin;

        public enum DamageType
        {
            Impact,
            Piercing,
        }

        public enum Origin
        {
            Enemy,
            Player
        }
    }
}