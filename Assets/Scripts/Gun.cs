using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Vector3 muzzleRotation;
    public Vector3 muzzleOffset;
    public float fireRate;
    public float power;

    private void Awake()
    {
        print(gameObject.name);
    }

    public void OnAttack()
    {
        FireShot();
    }

    public void FireShot()
    {
        var shot = Instantiate(bulletPrefab, gameObject.transform.position + muzzleOffset, Quaternion.identity);
        shot.GetComponent<Projectile>().Initialise(Camera.main, muzzleRotation * power);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.darkRed;

        var size = Vector3.one * 2;
        size.z *= 4;

        Gizmos.DrawCube(gameObject.transform.position + muzzleOffset, size);
    }
}