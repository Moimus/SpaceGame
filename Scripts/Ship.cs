﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IHitable, IDestructable
{
    [Header("Weapons")]
    //Weapons
    public Weapon weapon;
    /*
    public GameObject bulletSpawner;
    public GameObject projectile;
    */

    [Header("ControlVariables")]
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
    public float yawSpeed = 200; //Controller only

    [Header("Sheet")]
    //Sheet
    public int hpMax = 10;
    public int hpCurrent;
    public float energyMax = 100f;
    public float energyCurrent;
    public float energyRechargeRate = 10.0f; //how much energy is recharged per second
    public float fuelMax = 100;
    public float fuelCurrent;

    [Header("FX")]
    //FX
    public GameObject trail;
    public float trailSpeed = 1; //used by autotrail to determine at which speed the trail appears & dissappears
    public Camera mainCamera;
    public float deathDelay = 2.0f; //how long the ship is alive after onDestroy() is called
    public GameObject explosionFx;

    //Scanner
    public GameObject scanPrefab;

    // Start is called before the first frame update
    void Start()
    {
        hpCurrent = hpMax;
        energyCurrent = energyMax;
        fuelCurrent = fuelMax;

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

        rechargeEnergy();
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

    //TODO
    
    public void fireWeapon()
    {
        if (weapon != null)
        {
                weapon.fire();
        }
    }
    
    public void scan()
    {
        GameObject scan = Instantiate(scanPrefab, transform.position, transform.rotation, transform);
    }

    protected void rechargeEnergy()
    {
        if (energyCurrent < energyMax)
        {
            energyCurrent += energyRechargeRate * Time.deltaTime;
        }
    }

    //automatically toggles the ships trail
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

    private void OnCollisionEnter(Collision collision)
    {
        handleCollision();
    }

    public void onHit(int damage)
    {
        hpCurrent -= damage;
        checkAlive();
    }

    public void onHit()
    {
        Debug.Log("Hit!");
    }

    //called by onhit(...), checks if the player has more than 0 HP and destroys the player otherwise(calls IDestructable.onDestroy())
    protected void checkAlive()
    {
        if(hpCurrent <= 0)
        {
            onDestroy();
        }
    }

    public void onDestroy()
    {
        mainCamera.transform.parent = null; // decouples the camera from the player ship so it won't get destroyed

        GameObject explosion = Instantiate(explosionFx, transform.position, transform.rotation); //spawn an explosion
        Destroy(gameObject, deathDelay); //Destroy the ship object
    }

    //This is what happens when the ship crashes into something, called by OnCollisionEnter, this is a seperate method so you can override it in child classes for a different behaviour
    protected virtual void handleCollision()
    {
        if(speedCurrent > 2)
        {
            int crashDamage = (int)speedCurrent * 2;
            speedCurrent = 0;
            onHit(crashDamage);
        }
    }
    
    //fun method for changing the field-of-view, maybe use this for a hyperdrive effect later
    public void setFOV(int amount)
    {
        mainCamera.fieldOfView = amount;
    }

    //Gamepad related functions
    public void yawDown(float factor)
    {
        transform.Rotate(Vector3.right, yawSpeed * factor * yawSensitivity * Time.deltaTime);
    }

    public void yawUp(float factor)
    {
        transform.Rotate(-Vector3.right, yawSpeed * factor * yawSensitivity * Time.deltaTime);
    }

    public void yawLeft(float factor)
    {
        transform.Rotate(-Vector3.up, yawSpeed * factor * yawSensitivity * Time.deltaTime);
    }

    public void yawRight(float factor)
    {
        transform.Rotate(Vector3.up, yawSpeed * factor * yawSensitivity * Time.deltaTime);
    }
}
