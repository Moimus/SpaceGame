using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //Weapons
    public GameObject bulletSpawner;
    public GameObject projectile;

    //ControlVariables
    public float speedCurrent = 0f;
    public float speedMax = 3f;
    public float acceleration = 1f;
    public float deceleration = 1f;
    bool cruiseMode = false;

    public float rollSpeedMax = 100; //Maximum roll speed
    public float rollSpeedCurrent = 0f; //current roll speed
    public float rollAcceleration = 200f; //rate of accelleration
    public float rollDeceleration = 400f; //rate of deceleration

    public float yawSensitivity = 0.5f;

    //Sheet
    //TODO

    //FX
    public GameObject trail;
    public float trailSpeed = 1; //used by autotrail to determine at which speed the trail appears & dissappears

    //Scanner
    public GameObject scanPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stateLoop()
    {
        if (speedCurrent >= 0f)
        {
            moveForward();
        }

        if(cruiseMode)
        {
            speedUp();
        }

        roll();
        autoTrail();
    }

    public void lookAtMouse()
    {
        float fx = Math.cubed((Input.mousePosition.x - 1000f) / (90));
        transform.Rotate(Vector3.up, fx * yawSensitivity * Time.deltaTime);
        float fy = Math.cubed(-(Input.mousePosition.y - 600f) / (90));
        transform.Rotate(Vector3.right, fy * yawSensitivity * Time.deltaTime);
    }

    public void moveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speedCurrent);
    }

    public void toggleCruiseMode()
    {
        if(!cruiseMode)
        {
            cruiseMode = true;
        }
        else
        {
            cruiseMode = false;
        }
    }

    public void speedUp()
    {
        if (speedCurrent < speedMax)
        {
            speedCurrent += 1 * Time.deltaTime * acceleration;
        }
    }

    public void slowDown()
    {
        if (speedCurrent > 0)
        {
            speedCurrent -= 1 * Time.deltaTime * deceleration;

        }
    }

    //called every frame to roll the ship
    public void roll()
    {
        transform.Rotate(Vector3.forward, rollSpeedCurrent * Time.deltaTime);
    }

    public void rollRight()
    {
        if (rollSpeedCurrent > -rollSpeedMax)
        {
            rollSpeedCurrent -= Time.deltaTime * rollAcceleration;
        }
    }

    public void rollLeft()
    {
        if (rollSpeedCurrent < rollSpeedMax)
        {
            rollSpeedCurrent += Time.deltaTime * rollAcceleration;
        }
    }

    public void rollIdle()
    {
        if (rollSpeedCurrent > 0 && rollSpeedCurrent > 0.01f)
        {
            rollSpeedCurrent -= Time.deltaTime * rollDeceleration;
        }
        else if (rollSpeedCurrent < 0 && rollSpeedCurrent < -0.01f)
        {
            rollSpeedCurrent += Time.deltaTime * rollDeceleration;
        }
        else
        {

        }
    }

    public void fire()
    {
        GameObject p = Instantiate(projectile);
        p.GetComponent<Projectile>().owner = gameObject.transform;
        p.transform.position = bulletSpawner.transform.position;
        p.transform.rotation = bulletSpawner.transform.rotation;
    }

    public void scan()
    {
        GameObject scan = Instantiate(scanPrefab, transform.position, transform.rotation, transform);
    }

    protected void autoTrail()
    {
        if(speedCurrent > trailSpeed && !trail.activeInHierarchy)
        {
            showTrail();
        }
        else if(speedCurrent < trailSpeed && trail.activeInHierarchy)
        {
            hideTrail();
        }
    }

    public void showTrail()
    {
        trail.SetActive(true);
    }

    public void hideTrail()
    {
        trail.SetActive(false);
    }
}
