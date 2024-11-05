using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
    [SerializeField] List<GameObject> waypoints;
    [SerializeField] float moveSpeed = 5f;
    int waypointIndex = 0;
    

    void Start()
    {
        gameObject.transform.position = waypoints[waypointIndex].transform.position;
    }

    
    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].transform.position;
            float delta = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta); //move towards the transform of the current waypointindex element position, if we are already there, this is dealt with below.
            if ((Vector3)transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject); //this should delete the instance of the object once it reaches the final waypoint. 

            //TODO - implement looping through waypoints till destroyed or time limit achieved instead.
        }
    }
}
