using System.Collections.Generic;
using Gun.Api;
using Player.Scripts;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IShootable
{
    [Header("Waypoints")] [SerializeField] public WaypointTrack waypointTrack;
    [SerializeField] public List<Transform> waypointPath;

    [Header("Enemy Variables")] [SerializeField]
    public float speed = 50f; //alterable in inspector

    [SerializeField] public int maxHitpoints = 10; //alterable in inspector
    [SerializeField] public int maxPassCount = 0; //might be moved or changed by generator logic.
    [SerializeField] public int damageForHittingPlayer = 10;

    [Header("Enemy Values")] [SerializeField]
    public int scoreValue = 100;

    int nextWaypoint = 0;
    int passCount = 0;
    public int hitpoints;
    public bool isDestroyed = false;

    Leveller leveller;

    bool returnPath = false;
    public bool iAmAlive = false;

    private Rigidbody rb;
    private MeshCollider MeshCollider;

    private void Awake()
    {
        //waypointPath = new List<Transform>(); only needed if dropping enemy directly into scene
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        hitpoints = maxHitpoints;
        leveller = GetComponent<Leveller>();
    }

    private void Update()
    {
        if (!iAmAlive)
        {
            DestroyMe();
            return;
        }
    }

    private void FixedUpdate()
    {
        if (iAmAlive)
        {
            //move towards waypoint record.
            CheckAndMove();
        }
    }

    public void CreatePath(WaypointTrack path)
    {
        waypointPath = new List<Transform>();
        waypointPath.Clear(); //clears path
        waypointTrack = path; //assigns variable to waypoint track (avoids us having to set it up first)

        for (int i = 0; i < path.waypoints.Count; i++)
        {
            waypointPath.Add(path.waypoints[i]); //assigns transforms in order to the waypointpath
        }

        //foreach (Transform t in waypointPath)
        //{
        //    Debug.Log($"<color=green> {t.transform.position} </color>");
        //}

        //path now created. We now need to tell the enemy to start moving towards each waypoint.
        BeginPath();
    }

    private void BeginPath()
    {
        gameObject.transform.position = waypointPath[0].transform.position;
        nextWaypoint++;
        Debug.Log($"<color=cyan> Begin Path </color>");
    }

    private void CheckAndMove()
    {
        if (waypointPath == null || waypointPath.Count == 0)
            return;

        Vector3 target = waypointPath[nextWaypoint].position;
        target.y = leveller.player.yPos;

        Vector3 newPos = Vector3.MoveTowards(rb.position, target, (speed * Time.deltaTime));
        rb.MovePosition(newPos);
        //Debug.Log($"<color=#ff5c00> MovePosition tried</color>");


        if (passCount < maxPassCount)
        {
            if (Vector3.Distance(rb.position, target) < 0.1f)
            {
                if (!returnPath)
                {
                    if (nextWaypoint < waypointPath.Count - 1)
                    {
                        nextWaypoint++;
                        Debug.Log($"<color=#d912fb> moving forward </color>");
                    }
                    else
                    {
                        // Reached final waypoint
                        returnPath = true;
                        passCount++;
                        Debug.Log($"<color=#d912fb> reversing at end. Passcount is {passCount} </color>");
                    }
                }
                else
                {
                    if (nextWaypoint > 0)
                    {
                        nextWaypoint--;
                        Debug.Log($"<color=#d912fb> moving back </color>");
                    }
                    else
                    {
                        // Reached first waypoint
                        returnPath = false;
                        passCount++;
                        Debug.Log($"<color=#d912fb> reversing at start. Passcount is {passCount} </color>");
                    }
                }
            }
        }

        if (passCount >= maxPassCount)
        {
            Debug.Log($"Enemy <color=red> DESTROYED </color> due to max pass");
            DestroyMe();
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
            HandlePlayerCollision();
        else 
            Debug.Log($"registering trigger collision with {coll}");
    }

    private void HandlePlayerCollision()
    {
        Debug.Log($"<color=orange> registering player collision</color>");
        TakeDamage(damageForHittingPlayer);
    }

    public void TakeDamage(int dmg)
    {
        //add in call to animator to show hit effect here
        Debug.Log(
            $"{gameObject.name} taking <color=green> {dmg} </color> damage to <color=cyan>{hitpoints} </color>total HP");
        hitpoints -= dmg;

        if (hitpoints <= 0)
        {
            
            hitpoints = 0;

            if (isDestroyed) { return; }
            else
            {
                PlayerManager.Instance.AddToScore(scoreValue);
                isDestroyed = true;
                Debug.Log("ENEMY KILLED");
            }
            DestroyMe();
        }
        else
        {
            Debug.Log(
                $"{gameObject.name} now has <color=orange>{hitpoints}</color> out of <color=cyan>{maxHitpoints}</color> total HP.");
        }
    }

    public void DestroyMe()
    {              
        //add a call to explosion animation here with a wait
     
        iAmAlive = false;

        Debug.Log($"Enemy Destroyed.");
        Destroy(gameObject);
    }
}