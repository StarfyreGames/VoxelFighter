using Guns;
using System.Net.NetworkInformation;
using UnityEngine;
using utils;

public class Projectile : MonoBehaviour
{
    public BulletEntitySpec BulletEntitySpec { get; private set; }
    private GameObject _bullet;
    
    public void Initialise(BulletEntitySpec bulletEntityCharacteristics, GameObject bullet)
    {
        BulletEntitySpec = bulletEntityCharacteristics;
        _bullet = bullet;

        _bullet.transform.localScale = BulletEntitySpec.scale;
    }

    private void Update()
    {
        gameObject.transform.position += BulletEntitySpec.velocity * Time.deltaTime;
        if (CleanUpFence.ShouldDestroy(gameObject)) Destroy(gameObject);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}