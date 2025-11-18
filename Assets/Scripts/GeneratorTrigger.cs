using System.Collections;
using UnityEngine;

public class GeneratorTrigger : MonoBehaviour
{
    [SerializeField] public EnemyGenerator activateGenerator;
    [SerializeField] public GameObject EnemyToStart;
    [SerializeField] public WaypointTrack trackToFollow;
    [SerializeField] public float spawnDelay;
    [SerializeField] public int spawnTotal;

    private void Start()
    {
        //Vector3 myPos = transform.position;
        //myPos.y = PlayerScript.Instance.yPos;
        //transform.position = myPos;

    }

    private void Update()
    {
        //Vector3 myPos = transform.position;
        //myPos.y = PlayerScript.Instance.yPos;
        //transform.position = myPos;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (!activateGenerator.isRunning)
            {
                Debug.Log($"<color=orange> player encountered</color>");
                activateGenerator.GenerateEnemies(EnemyToStart, trackToFollow, spawnDelay, spawnTotal);
            }
            else
            {
                Debug.Log("Not Spawning as already triggered");
            }
        }
    }
}
