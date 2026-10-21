using Player.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpEffect", menuName = "Scriptable Objects/PowerUpEffect")]
public class PowerUpEffect : ScriptableObject
{
    public virtual void ApplyEffect(PlayerManager player)
    {
        // Implement the effect logic here
        Debug.Log($"Applying {name} effect to {player.player.name}");
    }
}
