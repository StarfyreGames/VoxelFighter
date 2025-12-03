using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] public GameObject[] EnemyOptions; //for enemy prefabs
    [SerializeField] public WaypointTrack[] WaypointTracks; //for waypoints track possibilities
    [SerializeField] public Vector3 spawnPosition;
    Quaternion spawnRotation = Quaternion.Euler(0,180,0);
    public GameObject enemyToSpawn;
    public EnemyScript spawnedEnemyScript;
    public bool isRunning { get; set; }

    private void Update()
    {
               
    }

    public void SetGeneratorOptions(GameObject[] EnemyOptions, WaypointTrack[] waypointTracks)
    {
        this.EnemyOptions = EnemyOptions;
        this.WaypointTracks = waypointTracks;

    }

    public void GenerateEnemies(float delay, int spawnTotal, int passCountOverride)
    {
        int rand1 = Random.Range(0, EnemyOptions.Length); //randomise what enemy shows up
        int rand2 = Random.Range(0, WaypointTracks.Length);
        
        enemyToSpawn = EnemyOptions[rand1];
        WaypointTrack enemyTrack = WaypointTracks[rand2];

        if(!isRunning)
            StartCoroutine(GenerationCycle(enemyToSpawn, enemyTrack, delay, spawnTotal, passCountOverride));

        
    }

    public void GenerateEnemies(GameObject enemyType, WaypointTrack followPath, float delay, int spawnTotal)
    {
        // this will take in a specific enemy type and waypoint track to spawn an enemy on. 
        // im still weighing up the value of having predictable enemy spawns. Either way its here.
        
        spawnPosition = transform.position;

        if(!isRunning)
            StartCoroutine(GenerationCycle(enemyType, followPath, delay, spawnTotal));

    }

    public void GenerateEnemies(GameObject enemyType, WaypointTrack followPath, float delay, int spawnTotal, int passCountOverride)
    {     
        spawnPosition = transform.position;

        if (!isRunning)
            StartCoroutine(GenerationCycle(enemyType, followPath, delay, spawnTotal, passCountOverride));

    }

    IEnumerator GenerationCycle(GameObject enemy, WaypointTrack path, float delay, int spawnTotal)
    {
        isRunning = true;
        for (int i = 0; i < spawnTotal; i++)
        {
            Debug.Log($"<color=blue> Generate called.</color>");
            GameObject newEnemy = Instantiate(enemy, spawnPosition, spawnRotation);
            spawnedEnemyScript = newEnemy.GetComponent<EnemyScript>();
            spawnedEnemyScript.waypointTrack = path;
            spawnedEnemyScript.CreatePath(path);
            spawnedEnemyScript.iAmAlive = true;
            Debug.Log("Instantiate");
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(2f);
        isRunning = false;
        Debug.Log($"<color=red> Generator Suspended");
    }

    IEnumerator GenerationCycle(GameObject enemy, WaypointTrack path, float delay, int spawnTotal, int passCount)
    {
        isRunning = true;
        for (int i = 0; i < spawnTotal; i++)
        {
            Debug.Log($"<color=blue> Generate called.</color>");
            GameObject newEnemy = Instantiate(enemy, spawnPosition, spawnRotation);
            spawnedEnemyScript = newEnemy.GetComponent<EnemyScript>();
            spawnedEnemyScript.waypointTrack = path;
            spawnedEnemyScript.CreatePath(path);
            spawnedEnemyScript.iAmAlive = true;
            spawnedEnemyScript.maxPassCount = passCount;
            Debug.Log("Instantiate");
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(2f);
        isRunning = false;
        Debug.Log($"<color=red> Generator Suspended");
    }


}
