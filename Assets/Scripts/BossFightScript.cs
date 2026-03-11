using UnityEngine;
using System.Collections;
using Player.Scripts;
using UnityEngine.UI;
using System.Net;


public class BossFightScript : MonoBehaviour
{
    [SerializeField] public BossGenerator bossGenerator;
    [SerializeField] EnemyGenerator enemyGenerator;
    [SerializeField] public Slider BossMeter;
    

    [Header("details for Adds")]
    [SerializeField] public GameObject[] EnemyOptions; //for enemy prefabs
    [SerializeField] public WaypointTrack[] WaypointTracks; //for waypoints track possibilities
    [SerializeField] public float spawnDelay;
    [SerializeField] public int spawnTotal;
    [SerializeField] public int passCountOverride;

    bool bossActive = false;
    bool isTriggered = false;
   
    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered)
            return;

        if (other.CompareTag("Player"))
        {
            if (!bossGenerator.isRunning)
            {
                isTriggered = true;

                Debug.Log($"<color=orange> player encountered, for boss</color>");
                Debug.Log($"Boss Generator is : {bossGenerator.name}");

                StartCoroutine(BossFightOne());

            }
            else
            {
                Debug.Log("Not Spawning as already triggered");
            }
        }
    }

    private void Update()
    {        

        if (bossActive)
        {
            bossGenerator.spawnedEnemyScript.OnBossDied += () => { bossActive = false; };
            BossMeter.value = (float)bossGenerator.newBoss.GetComponent<BossEnemy>().hitpoints / bossGenerator.newBoss.GetComponent<BossEnemy>().maxHitpoints;
        }
        else
        { }
    }


    public IEnumerator BossFightOne()
    {
        //ChangeMusic
        bossGenerator.GenerateBoss();

        BossMeter.value = (float)bossGenerator.newBoss.GetComponent<BossEnemy>().hitpoints/ bossGenerator.newBoss.GetComponent<BossEnemy>().maxHitpoints;

        //subscribe to ondeath event
        bossGenerator.spawnedEnemyScript.OnBossDied += () => { bossGenerator.bossActivated = false; };
        
        bossActive = true;
        GameManager.Instance.BossMeter.SetActive(true);

        //wait for bossActivated flag flip
        yield return new WaitWhile(() =>  bossGenerator.bossActivated);
        GameManager.Instance.BossMeter.SetActive(false);
        bossActive = false;
        enemyGenerator.isRunning = false;
        StartCoroutine(PlayerManager.Instance.EndGame());
    }
}
