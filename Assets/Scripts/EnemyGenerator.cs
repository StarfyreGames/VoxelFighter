using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] public GameObject[] EnemyOptions; //for enemy prefabs
    [SerializeField] public WaypointTrack[] WaypointTracks; //for waypoints track possibilities
    [SerializeField] public Vector3 spawnPosition;
    Quaternion spawnRotation = Quaternion.Euler(0,180,0);
    public GameObject enemyToSpawn;
    public EnemyScript spawnedEnemyScript;

    public void GenerateEnemies()
    {
        int rand1 = Random.Range(0, EnemyOptions.Length); //randomise what enemy shoes up
        int rand2 = Random.Range(0, WaypointTracks.Length);

        enemyToSpawn = EnemyOptions[rand1];        
        spawnedEnemyScript = enemyToSpawn.GetComponent<EnemyScript>();        
        spawnedEnemyScript.waypointTrack = WaypointTracks[rand2];

        spawnedEnemyScript.CreatePath(spawnedEnemyScript.waypointTrack);
        Instantiate(enemyToSpawn, spawnPosition, spawnRotation);
        spawnedEnemyScript.iAmAlive = true;
        enemyToSpawn.SetActive(true);
    }

    public void GenerateEnemies(GameObject enemyType, WaypointTrack followPath)
    {
        // this will take in a specific enemy type and waypoint track to spawn an enemy on. 
        // im still weighing up the value of having predictable enemy spawns. Either way its here.
    }




}
