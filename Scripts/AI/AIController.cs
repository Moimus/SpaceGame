using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    enum states
    {
        dead = -1,
        idle = 0,
        followRoute = 1
    }

    public int state = 0;
    public Ship ship;
    public IAIPackage activeAIPack;
    public AIFollowWaypoints followWaypointPack;
    
    // Start is called before the first frame update
    void Start()
    {
        setState((int)states.followRoute);
    }

    // Update is called once per frame
    void Update()
    {
        stateLoop();
    }

    public void stateLoop()
    {
        if(activeAIPack != null)
        {
            activeAIPack.run();
        }
    }

    public void setState(int state)
    {
        this.state = state;
        if(state == 0 || state == -1)
        {
            activeAIPack = null;
        }
        else if(state == 1)
        {
            activeAIPack = followWaypointPack;
            followWaypointPack.activate();
        }

    }
}
