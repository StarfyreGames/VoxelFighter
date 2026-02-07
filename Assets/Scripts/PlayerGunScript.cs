using System;
using System.Collections.Generic;
using Guns;
using Guns.Modifications;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour, IModifiable
{
    public List<Vector3> gunSlots;

    private Armament _armament;

    private void Awake()
    {
        var catalogue = GetComponent<BulletCatalogue>();
        _armament = new Armament(gunSlots, Vector3.forward, BulletEntitySpec.Origin.Player, catalogue, gameObject);

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

    public void ApplyModification(AModification modification)
    {
        _armament.ApplyModification(modification);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
            HandleApplyPowerUp(other.gameObject);
    }

    private void HandleApplyPowerUp(GameObject powerUp)
    {
        var powerUpScript = powerUp.GetComponent<PowerUpScript>();
        powerUpScript.ApplyPowerUpTo(this);
    }
}