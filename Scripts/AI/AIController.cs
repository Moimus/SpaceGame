using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum states
    {
        dead = -1,
        idle = 0,
        followRoute = 1,
        aggressiveMelee = 2,
        aggressiveRanged = 3
    }

    public enum personalities
    {
        stupid = 0,
        passive = 1,
        defensive = 2,
        aggressive = 3
    }

    public int personality = (int)personalities.stupid;
    public int state = (int)states.idle;
    public float awarenessRange = 5f;
    public List<Ship> awareOfShips = null;
    public int fallbackState = (int)states.idle; //state to use if the chosen AIPack doesn't exist
    public Ship ship;
    public IAIPackage activeAIPack;
    public AIFollowWaypoints followWaypointPack = null;
    public AIAggressiveMelee meleePack = null;
    
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
        awarenessLoop();
        personalityLoop();
    }

    void personalityLoop()
    {
        if(personality == (int)personalities.stupid)
        {

        }
        else if(personality == (int)personalities.passive)
        {

        }
        else if (personality == (int)personalities.defensive)
        {

        }
        else if (personality == (int)personalities.aggressive)
        {
            if(enemiesInRange())
            {
                setState((int)states.aggressiveMelee);
            }
        }
    }

    void awarenessLoop()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, awarenessRange);
        List<Ship> ships = new List<Ship>();
        for(int n = 0; n < hitColliders.Length; n++)
        {
            if(hitColliders[n].gameObject.GetComponent<Ship>() != null && !this.ship.Equals(hitColliders[n].gameObject.GetComponent<Ship>()))
            {
                ships.Add(hitColliders[n].gameObject.GetComponent<Ship>());
            }
        }
        awareOfShips = ships;
    }

    bool enemiesInRange()
    {
        bool enemyFound = false;
        if(awareOfShips != null && awareOfShips.Count > 0)
        {
            foreach(Ship s in awareOfShips)
            {
                if(s.faction != this.ship.faction)
                {
                    enemyFound = true;
                }
            }
        }
        return enemyFound;
    }

    public void setState(int state)
    {
        this.state = state;
        if(state == (int)states.idle || state == (int)states.dead)
        {
            activeAIPack = null;
        }
        else if(state == 1)
        {
            if(followWaypointPack != null)
            {
                activeAIPack = followWaypointPack;
                followWaypointPack.activate();
            }
            else
            {
                state = fallbackState;
            }
        }
        else if (state == 2)
        {
            if (meleePack != null)
            {
                activeAIPack = meleePack;
                meleePack.activate();
            }
            else
            {
                state = fallbackState;
            }
        }

    }
}
