using System.Collections.Generic;
using System.Linq;
using Persistence.Savable;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Gun.Persistence
{
    public class GunSlotEntity : Savable<GunSlotEntity>
    {
        public int Slot { get; set; }

        public string WeaponPrefab { get; set; }

        // === Relationships

        [Indexed(Unique = false)] public int GunRackId { get; set; }

        [Ignore]
        public BelongsTo<GunRackEntity> AttachedTo =>
            BelongsTo<GunRackEntity>("AttachedTo", () => GunRackId, id => GunRackId = id);

        [Ignore]
        public HasOne<BulletEntity> Bullet => HasOne(
            "Bullet",
            () => IBulletEntityRepository.GetForGunSlot(this),
            bullet => bullet.AttachedTo.Set(this)
        );

        [Ignore]
        public HasOne<FireModeEntity> FireMode => HasOne(
            "FireMode",
            () => IFireModeRepository.GetForGunSlot(this),
            fireMode => fireMode.AssignedTo.Set(this)
        );
    }

    public interface IGunSlotRepository : ISavableRepository<GunSlotEntity>
    {
        public static List<GunSlotEntity> GetForGunRack(GunRackEntity owner) =>
            Table.Where(save => save.GunRackId == owner.ID).ToList();
    }
}