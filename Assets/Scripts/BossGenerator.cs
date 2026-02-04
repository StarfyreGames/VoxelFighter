using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    
    [SerializeField] public GameObject BossPrefab;
    [SerializeField] public TextMeshProUGUI BossName;
    public GameObject newBoss;
    public string bossName;



    [SerializeField] public WaypointTrack BossTrack; //want a better way for this. Boss movements should be predictable but the sequence should be multifaceted for example phase one- boss moves down left, down right, left to right, wave in : Phase Two - boss moves in circle around screen, then returns to left /right, with occasional moves towards the player (so they can avoid it)
    [SerializeField] public EnemyGenerator EnemyGenerator;
    [SerializeField] public Vector3 spawnPosition;
    
    Quaternion bossRotation = Quaternion.Euler(90, 0, 0);

    public BossEnemy spawnedEnemyScript;
    
    public bool bossActivated = false;
    public bool isRunning { get; private set; }

    private void Update()
    {

    }

    //public void GenerateEnemies()
    //{
    //    int rand1 = Random.Range(0, EnemyOptions.Length); //randomise what enemy shows up
    //    int rand2 = Random.Range(0, WaypointTracks.Length);

    //    enemyToSpawn = EnemyOptions[rand1];

    //    GameObject newEnemy = Instantiate(enemyToSpawn, spawnPosition, spawnRotation);
    //    spawnedEnemyScript = newEnemy.GetComponent<EnemyScript>();
    //    spawnedEnemyScript.waypointTrack = WaypointTracks[rand2];
    //    spawnedEnemyScript.CreatePath(spawnedEnemyScript.waypointTrack);
    //    spawnedEnemyScript.iAmAlive = true;

    //}

    public void GenerateBoss()
    {
        Debug.Log($"Called boss generate, Boss is {BossPrefab.name}, Spawn Position is {spawnPosition}, rotation will be {bossRotation}");
        if (!bossActivated)
        {
            BossName.text = "";
            newBoss = Instantiate(BossPrefab, spawnPosition, bossRotation);
            bossName = BossPrefab.name;
            Debug.Log($"Boss generated - {bossName}");
            spawnedEnemyScript = newBoss.GetComponent<BossEnemy>();

            //subscribe to boss death event
            spawnedEnemyScript.OnBossDied += HandleBossDeath;

            Debug.Log($"Boss Script - {spawnedEnemyScript}");
            spawnedEnemyScript.waypointTrack = BossTrack;
            spawnedEnemyScript.CreatePath(spawnedEnemyScript.waypointTrack);
            spawnedEnemyScript.iAmAlive = true;
            bossActivated = true;
            BossName.text = bossName;
        }
    }

    private void HandleBossDeath()
    {
        Debug.Log("Boss Death Detected!");
        bossActivated = false;
    }

    // IEnumerator WaitDeath()
    //{

    //    yield return new WaitWhile(() => newBoss.GetComponent<BossEnemy>().iAmAlive);
    //    bossActivated = false;
    //}


}
