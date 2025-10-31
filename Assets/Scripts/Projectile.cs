using Guns;
using UnityEngine;
using utils;

public class Projectile : MonoBehaviour
{
    private BulletSpec _bulletSpec;
    private GameObject _bullet;

    public void Initialise(BulletSpec bulletCharacteristics, GameObject bullet)
    {
        _bulletSpec = bulletCharacteristics;
        _bullet = bullet;

        _bullet.transform.localScale = _bulletSpec.scale;
    }

    private void Update()
    {
        gameObject.transform.position += _bulletSpec.velocity * Time.deltaTime;
        if (CleanUpFence.ShouldDestroy(gameObject)) Destroy(gameObject);
    }
}