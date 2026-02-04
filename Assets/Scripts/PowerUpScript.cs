using System;
using System.Collections.Generic;
using System.Linq;
using Guns.Modifications;
using UnityEngine;
using UnityEngine.Serialization;

public class PowerUpScript : MonoBehaviour
{
    private List<AModification> _modifications;
    public bool isTriggered = false;


    private void Awake()
    {
        _modifications = GetComponents<AModification>().ToList();
    }

    public void ApplyPowerUpTo(IModifiable modifiable)
    {

        if (isTriggered)
            return;

        _modifications.ForEach(modifiable.ApplyModification);
        isTriggered = true;
        PlayerManager.Instance.pickUpInfo.text += $"\n\nApplying {gameObject.name} to craft";
        DestroyMe();
        
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}