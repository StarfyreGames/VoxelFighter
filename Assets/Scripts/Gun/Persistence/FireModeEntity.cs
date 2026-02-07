using System.Linq;
using Persistence.Savable;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Gun.Persistence
{
    public class FireModeEntity : Savable<FireModeEntity>, IFireModeRepository
    {
        public float FireRate { get; set; }

        public int NumberOfBullets { get; set; }

        public float SpreadAngle { get; set; }

        // === Relationships

        [Indexed(Unique = true)] public int GunSlotId { get; set; }

        [Ignore]
        public BelongsTo<GunSlotEntity> AssignedTo =>
            BelongsTo<GunSlotEntity>("AssignedTo", () => GunSlotId, id => GunSlotId = id);
    }

    public interface IFireModeRepository : ISavableRepository<FireModeEntity>
    {
        public static FireModeEntity GetForGunSlot(GunSlotEntity gunSlot) =>
            Table.FirstOrDefault(fm => fm.GunSlotId == gunSlot.ID);

        public static FireModeEntity PulseCannonDefault() => new()
        {
            FireRate = 0.5f,
            NumberOfBullets = 1,
            SpreadAngle = 0,
        };

        public static FireModeEntity SpreadCannonDefault() => new()
        {
            FireRate = 0.5f,
            NumberOfBullets = 2,
            SpreadAngle = 30
        };
    }
}