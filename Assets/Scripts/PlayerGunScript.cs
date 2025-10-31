using System.Collections.Generic;
using Guns;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour
{
    public List<Vector3> gunSlots;

    private Armament _armament;

    private void Awake()
    {
        var catalogue = GetComponent<BulletCatalogue>();
        _armament = new Armament(gunSlots, Vector3.forward, BulletSpec.Origin.Player, catalogue, gameObject);

        // Add the 2 starting guns
        _armament.AddGun();
        _armament.AddGun();
    }

    private void Update()
    {
        _armament.Update(Time.deltaTime);
    }

    public void OnAttack()
    {
        _armament.Fire();
    }

    private void OnDrawGizmos()
    {
        _armament.DrawGizmos();
    }
}