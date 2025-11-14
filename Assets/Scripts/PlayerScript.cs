using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float yPos = 0;
    public int shieldPoints; //we will also add an armor stat later ?

    [Header("Player Variables")]
    [SerializeField] public int maxShieldPoints = 5; //will replace or add to this with shields later
    [SerializeField] public int lives = 2;
    [SerializeField] GameObject playerPrefab;

    [Header("Other Variables")]
    [SerializeField] public GameObject Scroller;
    [SerializeField] public TerrainScroller actscroller;

    [Header("Unity UI Variables")]//will be moved to player manager
    [SerializeField] Slider shieldSlider;

    public static PlayerScript Instance { get; private set; }
    public bool iAmInvincible = false;
    public bool alreadyHit = false;
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
       
        shieldPoints = maxShieldPoints;

        shieldSlider.maxValue = maxShieldPoints;
        shieldSlider.value = shieldPoints;

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

    //----------------------------------The below needs to be refactored into a seperate manager--------------------------------------\\
    public void TakeDamage(int dmg)
    {
        //add in call to animator to show hit effect here
        //This will be changed to account for shielding later in the development. 
        //We should allow for upgrades to the players shields and hull etc - Tyrian
        if (iAmInvincible || alreadyHit)
        {
            Debug.Log($"<color=blue>Player can't take damage right now</color>");
        }
        else
        {
            Debug.Log($"{gameObject.name} taking <color=green> {dmg} </color> damage to <color=cyan>{shieldPoints} </color>total HP");
            shieldPoints -= dmg;

            if (shieldPoints <= 0)
            {
                shieldPoints = 0;
                Debug.Log("<color=orange>PLAYER KILLED</color>");
                //KillMe();
            }

            //Update UI SLIder
            shieldSlider.value = shieldPoints;
            Debug.Log($"{gameObject.name} now has <color=orange>{shieldPoints}</color> out of <color=cyan>{maxShieldPoints}</color> total HP.");
            StartCoroutine(StopMultiCrash());

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
        shieldPoints = maxShieldPoints;
        shieldSlider.value = shieldPoints;
        Debug.Log($"Player no longer invincible");
    }

    IEnumerator StopMultiCrash()
    {
        alreadyHit = true;
        yield return new WaitForSeconds(1.5f); //stops further collision registration
        alreadyHit = false;
    }

}

