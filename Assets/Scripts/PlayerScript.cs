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

    public static PlayerScript Instance { get; private set; }    

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
    }

    private void LateUpdate()
    {
        yPos = transform.position.y;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            EnemyScript enemy = coll.gameObject.GetComponent<EnemyScript>();
            enemy.TakeDamage(10); //standard damage for collision

            TakeDamage(2); //player also takes damage
            Debug.Log($"registered a collision with <color=red>{coll.gameObject.name}</color>.");
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        
    }
    public void TakeDamage(int dmg)
    {
        //add in call to animator to show hit effect here
        //This will be changed to account for shielding later in the development. 
        //We should allow for upgrades to the players shields and hull etc - Tyrian

        Debug.Log($"{gameObject.name} taking <color=green> {dmg} </color> damage to <color=cyan>{hitpoints} </color>total HP");
        hitpoints -= dmg;

        if (hitpoints <= 0)
        {
            hitpoints = 0;
            //Add score level to award player here
            Debug.Log("<color=orange>PLAYER KILLED</color>");
            DestroyMe();
        }
        else
        {
            Debug.Log($"{gameObject.name} now has <color=orange>{hitpoints}</color> out of <color=cyan>{maxHitpoints}</color> total HP.");
        }
    }

    public void DestroyMe()
    {
        //add a call to explosion animation here with a wait
        Vector3 myPos = transform.position;
        quaternion myRotation = Quaternion.identity;

        Destroy(gameObject);
        lives--;
        if (lives > 0) 
        {
            Instantiate(playerPrefab, myPos, myRotation);
        }
        else if (lives >= 0)
        {
            Debug.Log($"<color=red>PLAYER DESTROYED GAME OVER</color>");
        }
        
    }

}

