using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowWaypoints : MonoBehaviour, IAIPackage
{
    public bool patrolMode = false;
    public int patrolIndex = 0;
    public AIController controller;
    public List<Transform> waypoints;
    private Transform currentWaypoint;
    public float minWaypointDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void run()
    {
        if(patrolMode)
        {
            patrolWaypoints();
        }
        else
        {
            followWaypoints();
        }
    }

    public void activate()
    {
        if(waypoints.Count > 0)
        {
            currentWaypoint = waypoints[0];
        }
    }

    private void followWaypoints()
    {
        if(waypoints.Count > 0) //are there any waypoints?
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, currentWaypoint.position); //distance between AIShip & waypoint
            if(distanceToWaypoint > minWaypointDistance) //did we reach the waypoint?
            {
                controller.ship.lookAt(waypoints[0].position); //rotate the ship towards the waypoint
                controller.ship.speedUp(); //move the ship towards the waypoint
            }
            else //in case we reached the waypoint
            {
                waypoints.Remove(currentWaypoint); //discard old waypoint
                if(waypoints.Count > 0) //are there any waypoints left?
                {
                    currentWaypoint = waypoints[0]; // set the next waypoint
                }
            }
        }
        else
        {
            controller.ship.slowDown();
        }
    }

    private void patrolWaypoints()
    {
        if (waypoints.Count > 0) //are there any waypoints?
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, currentWaypoint.position); //distance between AIShip & waypoint
            if (distanceToWaypoint > minWaypointDistance) //did we reach the waypoint?
            {
                controller.ship.lookAt(currentWaypoint.position); //rotate the ship towards the waypoint
                controller.ship.speedUp(); //move the ship towards the waypoint
            }
            else //in case we reached the waypoint
            {
                if(patrolIndex < waypoints.Count - 1) // is the current waypointthe last in our list?
                {
                    patrolIndex++;
                    currentWaypoint = waypoints[patrolIndex];
                }
                else
                {
                    patrolIndex = 0; //start our route again
                    currentWaypoint = waypoints[patrolIndex];
                }
            }
        }
        else
        {
            controller.ship.slowDown();
        }
    }

}
