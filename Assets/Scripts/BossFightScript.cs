using UnityEngine;
using System.Collections;

public class BossFightScript : MonoBehaviour
{
    [SerializeField] public BossGenerator bossGenerator;
    [SerializeField] EnemyGenerator enemyGenerator;

    [Header("details for Adds")]
    [SerializeField] public GameObject[] EnemyOptions; //for enemy prefabs
    [SerializeField] public WaypointTrack[] WaypointTracks; //for waypoints track possibilities
    [SerializeField] public float spawnDelay;
    [SerializeField] public int spawnTotal;
    [SerializeField] public int passCountOverride;

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

    public IEnumerator BossFightOne()
    {
        //ChangeMusic
        bossGenerator.GenerateBoss();
        enemyGenerator.SetGeneratorOptions(EnemyOptions, WaypointTracks);
        yield return new WaitForSeconds(30f);
        enemyGenerator.GenerateEnemies(spawnDelay, spawnTotal, passCountOverride); //sends a random enemy to spawn
        yield return new WaitForSeconds(90f);
        enemyGenerator.isRunning = false;
        StartCoroutine(PlayerManager.Instance.EndGame());
    }
}
