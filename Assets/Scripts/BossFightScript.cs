using UnityEngine;
using System.Collections;
using UnityEngine.UI;


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!bossGenerator.isRunning)
            {
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
            BossMeter.value = (float)bossGenerator.newBoss.GetComponent<BossEnemy>().hitpoints / bossGenerator.newBoss.GetComponent<BossEnemy>().maxHitpoints;
        }
    }


    public IEnumerator BossFightOne()
    {
        //ChangeMusic
        bossGenerator.GenerateBoss();
        BossMeter.value = (float)bossGenerator.newBoss.GetComponent<BossEnemy>().hitpoints/ bossGenerator.newBoss.GetComponent<BossEnemy>().maxHitpoints;
        
        bossActive = true;
        GameManager.Instance.BossMeter.SetActive(true);
        //enemyGenerator.SetGeneratorOptions(EnemyOptions, WaypointTracks);
        //yield return new WaitForSeconds(30f);
        //enemyGenerator.GenerateEnemies(spawnDelay, spawnTotal, passCountOverride); //sends a random enemy to spawn
        //yield return new WaitForSeconds(90f); - needs to be wait till boss is dead
        yield return new WaitWhile(() =>  bossGenerator.bossActivated);
        GameManager.Instance.BossMeter.SetActive(false);
        bossActive = false;
        enemyGenerator.isRunning = false;
        StartCoroutine(PlayerManager.Instance.EndGame());
    }
}
