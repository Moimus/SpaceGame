using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAggressiveMelee : MonoBehaviour, IAIPackage
{
    enum states
    {
        engaging = 0,
        firing = 1,
        evading = 2,
        fleeing = 3
    }
    public AIController controller;
    public Ship[] sensedTargets;
    public Ship currentTarget = null;
    public float aggressionRange = 50f;
    public float attackRange = 25f;
    public float evadeRange = 5f;
    public float maxSpeed = 10;
    public float halfSpeed = 5;
    public int state = (int)states.engaging;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        maxSpeed = controller.ship.speedMax;
        halfSpeed = controller.ship.speedMax / 2;
    }

    public void activate()
    {

    }

    void cooldownLoop()
    {

    }

    void navigationLoop()
    {
        if(state == (int)states.engaging)
        {
            if (currentTarget != null)
            {
                controller.ship.lookAt(currentTarget.transform.position);
                controller.ship.speedUp();
            }
        }
        else if (state == (int)states.firing)
        {
            if (currentTarget != null)
            {
                controller.ship.lookAt(currentTarget.transform.position);
                controller.ship.fireWeapons();
            }
        }
        else if (state == (int)states.evading)
        {
            controller.ship.yawUp(controller.ship.yawSpeed * 0.04f);
            if(controller.ship.speedCurrent > halfSpeed)
            {
                controller.ship.slowDown();
            }
            controller.ship.rollLeft();
        }
        else if (state == (int)states.fleeing)
        {
            controller.ship.speedUp();
        }
    }

    public void run()
    {
        cooldownLoop();
        navigationLoop();
        if(currentTarget == null)
        {
            foreach(Ship n in controller.awareOfShips)
            {
                if(n != null)
                {
                    if (n.faction != controller.ship.faction)
                    {
                        currentTarget = n.GetComponent<Ship>();
                    }
                }
            }
        }
        else if(currentTarget != null)
        {
            float d = Vector3.Distance(transform.position, currentTarget.transform.position);
            if(state == (int)states.engaging && d < attackRange)
            {
                state = (int)states.firing;
            }
            else if((state == (int)states.firing || state == (int)states.evading) && d < evadeRange)
            {
                state = (int)states.evading;
            }
            else if(state == (int)states.evading && d > evadeRange && d < evadeRange * 6)
            {
                state = (int)states.fleeing;
            }
            else if (state == (int)states.fleeing && d > evadeRange * 6)
            {
                state = (int)states.engaging;
            }
            /*if ((state == (int)states.firing || state == (int)states.evading) && d < evadeRange)
            {
                state = (int)states.evading;
            }
            else if(state == (int)states.evading && d < evadeRange * 30)
            {
                state = (int)states.fleeing;
            }
            else if(state == (int)states.engaging && state != (int)states.fleeing && d < attackRange)
            {
                state = (int)states.firing;
            }
            else if(state == (int)states.engaging || state == (int)states.fleeing)
            {
                state = (int)states.engaging;
            }*/

        }
    }
}
