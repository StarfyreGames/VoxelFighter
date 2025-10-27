using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

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

    private void Start()
    {
        waypointPath = new List<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        if (iAmAlive)
        {
            Vector3 myPos = transform.position;
            myPos.y = PlayerScript.Instance.yPos;
            transform.position = myPos;

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
        waypointPath.Clear(); //clears path
        waypointTrack = path; //assigns variable to waypoint track (avoids us having to set it up first)

        for (int i = 0; i < path.waypoints.Count; i++)
        {
            waypointPath.Add(path.waypoints[i]); //assigns transforms in order to the waypointpath
        }
        
        foreach (Transform t in waypointPath)
        {
            Debug.Log($"<color = green> {t.transform.position} </color>");
        }

        //path now created. We now need to tell the enemy to start moving towards each waypoint.
        BeginPath();        
    }

    private void BeginPath()
    {
        gameObject.transform.position = waypointPath[0].transform.position;
        nextWaypoint++;
    }

    private void CheckAndMove()
    {
        if (waypointPath == null || waypointPath.Count == 0)
            return;

        Vector3 target = waypointPath[nextWaypoint].position;
        target.y = PlayerScript.Instance.yPos;

        Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);


        if (passCount < maxPassCount)
        {
            if (Vector3.Distance(rb.position, target) < 0.1f)
            {
                if (!returnPath)
                {
                    nextWaypoint++;
                }

                if (nextWaypoint == waypointPath.Count)
                {
                    returnPath = true;
                    nextWaypoint--;
                    passCount++;
                }
            }
            else if (returnPath)
            {
                if (Vector3.Distance(rb.position, target) < 0.1f)
                {
                    nextWaypoint--;
                }
                if (nextWaypoint < 0)
                {
                    returnPath = false;
                    nextWaypoint = 0;
                    passCount++;
                }
            }
        }
        
        if (passCount > maxPassCount)
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
