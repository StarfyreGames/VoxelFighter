using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float yPos = 0;
    public int hitpoints;

    [Header("Player Variables")]
    [SerializeField] public int maxHitpoints = 5; //will replace or add to this with shields later
    [SerializeField] public int lives = 2;
    [SerializeField] GameObject playerPrefab;

    [Header("Other Variables")]
    [SerializeField] public GameObject Scroller;
    [SerializeField] public TerrainScroller actscroller;

    public static PlayerScript Instance { get; private set; }
    public bool iAmInvincible = false;
    public bool engagedBoss = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
       
        hitpoints = maxHitpoints;

        Debug.Log($"Scroller = {Scroller}");
        actscroller = Scroller.GetComponent<TerrainScroller>();
    }

    private void LateUpdate()
    {
        yPos = transform.position.y;
    }

    private void OnCollisionEnter(Collision coll)
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            TakeDamage(2); //player also takes damage
            Debug.Log($"registered a collision with <color=red>{coll.gameObject.name}</color>.");
        }
    }
    public void TakeDamage(int dmg)
    {
        //add in call to animator to show hit effect here
        //This will be changed to account for shielding later in the development. 
        //We should allow for upgrades to the players shields and hull etc - Tyrian
        if (iAmInvincible)
        {
            Debug.Log($"<color=blue>Player can't take damage right now</color>");
        }
        else
        {
            Debug.Log($"{gameObject.name} taking <color=green> {dmg} </color> damage to <color=cyan>{hitpoints} </color>total HP");
            hitpoints -= dmg;

            if (hitpoints <= 0)
            {
                hitpoints = 0;
                Debug.Log("<color=orange>PLAYER KILLED</color>");
                //insert destroy me 
            }
            else
            {
                Debug.Log($"{gameObject.name} now has <color=orange>{hitpoints}</color> out of <color=cyan>{maxHitpoints}</color> total HP.");
            }
        }

    }

    public void KillMe()
    {
        //add a call to explosion animation here with a wait
        Vector3 myPos = transform.position;
        
        lives--;
        if (lives > 0) 
        {
            iAmInvincible = true;
            gameObject.SetActive(false);
            Debug.Log($"Player loses a Life!");
            StartCoroutine(LoseLife());
        }
        else if (lives >= 0)
        {
            Debug.Log($"<color=red>PLAYER DESTROYED GAME OVER</color>");
            Destroy(gameObject); //change to game over routine later
        }
        
    }

    IEnumerator LoseLife()
    {
        actscroller.scrolling = false;
        Debug.Log($"<color=green>Player Respawning</color>");
        yield return new WaitForSeconds(2f);
        //"respawn" player
        gameObject.SetActive(true);
        //check if fighting boss
        if(!engagedBoss) 
            actscroller.scrolling = true;
        else 
            actscroller.scrolling = false;

        yield return new WaitForSeconds(2f);
        //insert invulnerability flash here
        iAmInvincible=false;
        Debug.Log($"Player no longer invincible");
    }

}

