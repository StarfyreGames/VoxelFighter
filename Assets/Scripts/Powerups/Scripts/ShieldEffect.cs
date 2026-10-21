using Player.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldEffect", menuName = "Scriptable Objects/ShieldEffect")]
public class ShieldEffect : PowerUpEffect
{
    public int shieldPointsToAdd = 10;

    public override void ApplyEffect(PlayerManager player)
    {
        player.player.shieldPoints += shieldPointsToAdd;
        Debug.Log($"Applied shield effect to {player.player.name}");
        if(player.player.shieldPoints > player.player.maxShieldPoints)
        {
            player.player.shieldPoints = player.player.maxShieldPoints;            
        }
    }
}
