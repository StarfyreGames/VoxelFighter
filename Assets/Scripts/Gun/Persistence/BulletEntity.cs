using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Persistence;
using Persistence.Savable;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Gun.Persistence
{
    public class BulletEntity : Savable<BulletEntity>, IBulletEntityRepository
    {
        public float Velocity { get; set; }
        public float Damage { get; set; }
        public float Scale { get; set; }

        public int MaxPassthroughImpacts { get; set; }
        public float PassthroughDamageReductionFactor { get; set; }
        public float PassthroughFrictionFactor { get; set; }

        public string PrefabName { get; set; }

        // === Relationships

        [Indexed(Unique = true)] public int GunSlotId { get; set; }

        [Ignore]
        public BelongsTo<GunSlotEntity> AttachedTo =>
            BelongsTo<GunSlotEntity>("AttachedTo", () => GunSlotId, id => GunSlotId = id);
    }

    public interface IBulletEntityRepository : ISavableRepository<BulletEntity>
    {
        public static BulletEntity GetForGunSlot(GunSlotEntity gunSlot) =>
            Table.FirstOrDefault(b => b.GunSlotId == gunSlot.ID);

        public static BulletEntity CreatePulseCannonBullet(int damage) => new()
        {
            Damage = damage,
            MaxPassthroughImpacts = 1,
            PassthroughFrictionFactor = 0,
            PassthroughDamageReductionFactor = 1,
            PrefabName = "PCShot",
            Velocity = 150f,
            Scale = 3
        };

        public static BulletEntity CreatePulseCannon8() => CreatePulseCannonBullet(5);
        public static BulletEntity CreatePulseCannon10() => CreatePulseCannonBullet(10);
        public static BulletEntity CreatePulseCannon12() => CreatePulseCannonBullet(12);
        public static BulletEntity CreatePulseCannon15() => CreatePulseCannonBullet(15);
        public static BulletEntity CreatePulseCannon5() => CreatePulseCannonBullet(20);

        public static BulletEntity CreateSpreadPulseCannon5() => CreatePulseCannonBullet(5);
        public static BulletEntity CreateSpreadPulseCannon17() => CreatePulseCannonBullet(17);
        public static BulletEntity CreateSpreadPulseCannon10() => CreatePulseCannonBullet(10);
        public static BulletEntity CreateSpreadPulseCannon15() => CreatePulseCannonBullet(15);
    }
}