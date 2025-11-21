using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float yPos = 0;
    public int shieldPoints; //we will also add an armor stat later ?

    [Header("Player Variables")] [SerializeField]
    public int maxShieldPoints = 5; //will replace or add to this with shields later

    [SerializeField] public PlayerManager playerManager;


    //public static PlayerScript Instance { get; private set; }
    public bool iAmInvincible = false;
    public bool alreadyHit = false;
    public bool engagedBoss = false;

    private void Awake()
    {
        //if (Instance != null && Instance != this)
        //{
        //    Destroy(this);
        //}
        //else
        //{
        //    Instance = this;
        //}
        playerManager = PlayerManager.Instance;
        shieldPoints = maxShieldPoints;
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
        if (coll.gameObject.CompareTag("Enemy"))
        {
            playerManager.TakeDamage(5); //player also takes damage
            Debug.Log($"registered a collision with <color=red>{coll.gameObject.name}</color>.");
        }
    }
}