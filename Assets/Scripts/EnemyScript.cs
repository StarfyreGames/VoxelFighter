using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] public WaypointTrack waypointTrack;
    [SerializeField] public List<Transform> waypointPath;

    [Header("Enemy Variables")]
    [SerializeField] public float speed = 50f;
    [SerializeField] public int hitpoints = 2;
    [SerializeField] public int maxPassCount = 0; //might be moved or changed by generator logic.

    int nextWaypoint = 0;
    int passCount = 0;
    

    bool returnPath = false;
    public bool iAmAlive = false;

    private Rigidbody rb;

    private void Awake()
    {
        //waypointPath = new List<Transform>(); only needed if dropping enemy directly into scene
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        if (iAmAlive)
        {
            //move towards waypoint record.
            CheckAndMove();
        }
        else
        {
            DestroyMe();
            return;
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
        
        foreach (Transform t in waypointPath)
        {
            Debug.Log($"<color=green> {t.transform.position} </color>");
        }

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
        target.y = PlayerScript.Instance.yPos;

        Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);
        Debug.Log($"<color=#ff5c00> MovePosition tried</color>");

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


    public void TakeDamage(int dmg)
    {
        //add in call to animator to show hit effect here

        hitpoints -= dmg;

        if (hitpoints <= 0)
        {
            hitpoints = 0;
            //Add score level to award player here
            Debug.Log("ENEMY KILLED");
            DestroyMe();
        }
    }

    public void DestroyMe()
    {
        //add a call to explosion animation here with a wait
        iAmAlive = false;
        Destroy(gameObject);
    }
}
