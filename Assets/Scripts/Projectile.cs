using Guns;
using System.Net.NetworkInformation;
using UnityEngine;
using utils;

public class Projectile : MonoBehaviour
{
    private BulletSpec _bulletSpec;
    private GameObject _bullet;

    //added to expose variables
    public int damage;   
    public BulletSpec.Origin origin;

    public void Initialise(BulletSpec bulletCharacteristics, GameObject bullet)
    {
        _bulletSpec = bulletCharacteristics;
        _bullet = bullet;

        _bullet.transform.localScale = _bulletSpec.scale;

        damage = (int)_bulletSpec.damage;
        origin = _bulletSpec.origin;
    }

    private void Update()
    {
        gameObject.transform.position += _bulletSpec.velocity * Time.deltaTime;
        if (CleanUpFence.ShouldDestroy(gameObject)) Destroy(gameObject);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}