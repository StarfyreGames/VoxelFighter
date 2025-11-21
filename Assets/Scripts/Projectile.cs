using Guns;
using System.Net.NetworkInformation;
using UnityEngine;
using utils;

public class Projectile : MonoBehaviour
{
    public BulletSpec BulletSpec { get; private set; }
    private GameObject _bullet;
    
    public void Initialise(BulletSpec bulletCharacteristics, GameObject bullet)
    {
        BulletSpec = bulletCharacteristics;
        _bullet = bullet;

        _bullet.transform.localScale = BulletSpec.scale;
    }

    private void Update()
    {
        gameObject.transform.position += BulletSpec.velocity * Time.deltaTime;
        if (CleanUpFence.ShouldDestroy(gameObject)) Destroy(gameObject);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}