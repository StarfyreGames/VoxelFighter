using Player.Scripts;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public PowerUpEffect effect;

    public bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if(collected) return;

        if (other.tag == "Player")
        {
            Debug.Log("PowerUp collected: " + gameObject.name);
            collected = true;
            effect.ApplyEffect(PlayerManager.Instance);
            Destroy(gameObject);
        }   
    }

    /*
     * on trigger enter
     * 
     * if collected
     * 
     * if not collected
     *      collected true
     *      apply effect
     *      destroy gameobject
     *      
     *      
     */
}
