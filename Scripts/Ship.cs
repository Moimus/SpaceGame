﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Entity, IHitable, IDestructable, IExportable
{
    [Header("UI")]
    public bool hasUI = false;
    public PlayerUI ui;
    public MarkerUI markerUI;

    [Header("Weapons")]
    //Weapons
    public List<Weapon> weapons;
    public GameObject[] weaponMounts;

    [Header("ControlVariables")]
    //ControlVariables
    public bool overrideControls = false;
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

    int screenWidthHalf = Screen.width / 2;  //TODO move this to KeyboardController
    int screenHeightHalf = Screen.height / 2; //TODO move this to KeyboardController
    int deadZoneXPositive; //TODO move this to KeyboardController
    int deadZoneXNegative; //TODO move this to KeyboardController
    int deadZoneYPositive; //TODO move this to KeyboardController
    int deadZoneYNegative; //TODO move this to KeyboardController
    int displayAspectRatio; //TODO move this to KeyboardController

    [Header("Sheet")]
    //Sheet
    public Player player;
    public string displayName = "Ship";
    public float energyMax = 100f;
    public float energyCurrent;
    public float energyRechargeRate = 10.0f; //how much energy is recharged per second
    public float fuelMax = 100;
    public float fuelCurrent;
    public ShipSystem mountedSystem = null;

    [Header("FX")]
    //FX
    public GameObject trail;
    public float trailSpeed = 1; //used by autotrail to determine at which speed the trail appears & dissappears
    public Camera mainCamera;
    public float deathDelay = 2.0f; //how long the ship is alive after onDestroy() is called
    public GameObject explosionFx;

    //Scanner
    public GameObject scanPrefab;
    public List<Ship> scannedShips = new List<Ship>();
    public List<Marker> relatedMarkers = new List<Marker>(); // list of markers that mark this ship on other UIs
    public int selectedTargetPointer = 0;
    public Marker selectedTargetMarker;

    [Header("DataModel")]
    public string prefabPath;

    // Start is called before the first frame update
    void Start()
    {
        calcAspectRatio();
        calcDeadZones();
        hpCurrent = hpMax;
        energyCurrent = energyMax;
        fuelCurrent = fuelMax;
        //ShipModel m = toModel() as ShipModel;
        //m.export("testExport");
        //Debug.Log(mainCamera.WorldToScreenPoint(transform.position));
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        stateLoop();
        // Debug.Log(mainCamera.WorldToScreenPoint(transform.position));
        //Debug.Log(Input.mousePosition.x + "//" + Input.mousePosition.y);
    }

    public void construct(bool hasUI)
    {
        this.hasUI = hasUI;

    }

    public void stateLoop()
    {
        if (speedCurrent >= 0f)
        {
            moveForward();
        }

        if (cruiseMode)
        {
            speedUp();
        }

        roll();
        autoTrail();
        rechargeEnergy();
        updateUI();
    }

    void calcAspectRatio()
    {
        if(mainCamera != null)
        {
            displayAspectRatio = (int)mainCamera.aspect * Screen.width / Screen.height;
        }
    }

    //TODO move this to KeyboardController
    void calcDeadZones()
    {
        deadZoneXPositive = screenWidthHalf + screenWidthHalf / 10;
        deadZoneXNegative = screenWidthHalf - screenWidthHalf / 10;
        deadZoneYPositive = screenHeightHalf + screenHeightHalf / 10;
        deadZoneYNegative = screenHeightHalf - screenHeightHalf / 10;
    }

    [System.ObsoleteAttribute("This method is obsolete. Call followMouse() instead.", true)]
    public void lookAtMouse()
    {
        float fx = Math.cubed((Input.mousePosition.x - screenWidthHalf) / (90));
        transform.Rotate(Vector3.up, fx * yawSensitivity * Time.deltaTime);
        float fy = Math.cubed(-(Input.mousePosition.y - screenHeightHalf) / (90)) + displayAspectRatio;
        transform.Rotate(Vector3.right, fy * yawSensitivity * Time.deltaTime);
    }

    //TODO move this to KeyboardController
    public void followMouse()
    {
        if(Input.mousePosition.x < deadZoneXNegative)
        {
            float mouseMidDistance = screenWidthHalf - Input.mousePosition.x;
            float smoothFactor = mouseMidDistance / screenWidthHalf;
            //Debug.Log(smoothFactor + "= " + mouseMidDistance + "/" + screenWidthHalf);
            yawLeft(smoothFactor);
        }
        else if(Input.mousePosition.x > deadZoneXPositive)
        {
            float mouseMidDistance = screenWidthHalf - Input.mousePosition.x;
            float smoothFactor = mouseMidDistance / screenWidthHalf;
            //Debug.Log(-smoothFactor + "= " + mouseMidDistance + "/" + screenWidthHalf);
            yawRight(-smoothFactor);
        }

        if(Input.mousePosition.y < deadZoneYNegative)
        {
            float mouseMidDistance = screenHeightHalf - Input.mousePosition.y;
            float smoothFactor = mouseMidDistance / screenHeightHalf;
            //Debug.Log(smoothFactor + "= " + mouseMidDistance + "/" + screenWidthHalf);
            yawDown(smoothFactor);
        }
        else if(Input.mousePosition.y > deadZoneYPositive)
        {
            float mouseMidDistance = screenHeightHalf - Input.mousePosition.y;
            float smoothFactor = mouseMidDistance / screenHeightHalf;
            //Debug.Log(-smoothFactor + "= " + mouseMidDistance + "/" + screenWidthHalf);
            yawUp(-smoothFactor);
        }
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
        else if(Math.valueIsBetween(rollSpeedCurrent, -0.01f, 0.01f))
        {

        }
        else
        {

        }
    }

    public void activateWeapons()
    {
        if(weapons != null)
        {
            foreach (Weapon w in weapons)
            {
                w.active = true;
            }
        }
    }
    
    public void fireWeapons()
    {
        if (weapons != null)
        {
            foreach(Weapon w in weapons)
            {
                w.fire();
            }
        }
    }
    
    /// <summary>
    /// AI function to emulate a release of the fireButton
    /// </summary>
    public void releaseWeapons()
    {
        if(weapons != null && weapons.Count > 0)
        {
            foreach (Weapon w in weapons)
            {
                w.releaseFire();
            }
        }
    }

    public void scan()
    {
        GameObject scan = Instantiate(scanPrefab, transform.position, transform.rotation, transform);
        scan.GetComponent<Scan>().owner = this;
    }

    public void activateSystem()
    {
        if(mountedSystem != null)
        {
            mountedSystem.activate();
        }
    }

    public void cleanScannedShips() //removes missing ships from scannedShips after they got destroyed
    {
        foreach(Ship s in scannedShips)
        {
            if(s == null)
            {
                scannedShips.Remove(s);
            }
        }
    }
    public void nextTarget()
    {
        //Debug.Log("nextTarget");
        if (scannedShips.Count > 0)
        {
            if(selectedTargetPointer == -1)
            {
                selectedTargetPointer = 0;
            }

            if (selectedTargetMarker != null)
            {
                Destroy(selectedTargetMarker.gameObject);
            }

            if (selectedTargetPointer < scannedShips.Count -1)
            {
                selectedTargetPointer++;
            }
            else
            {
                selectedTargetPointer = 0;
            }
            markerUI.selectTarget(scannedShips[selectedTargetPointer]);
        }
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
        if(trail != null)
        {
            if (speedCurrent > trailSpeed && !trail.activeInHierarchy)
            {
                showTrail();
            }
            else if (speedCurrent < trailSpeed && trail.activeInHierarchy)
            {
                hideTrail();
            }
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
    public void checkAlive()
    {
        if(hpCurrent <= 0)
        {
            if(alive)
            {
                alive = false;
                onDestroy();
            }
        }
    }

    public void onDestroy()
    {
        if(mainCamera != null)
        {
            mainCamera.transform.parent = null; // decouples the camera from the player ship so it won't get destroyed
            Destroy(mainCamera.gameObject, 5f); // destroy the camera with some delay (deathCam)
        }

        GameObject explosion = Instantiate(explosionFx, transform.position, transform.rotation); //spawn an explosion
        if(player != null)
        {
            player.startRespawnCounter();
        }

        foreach(Marker m in relatedMarkers)
        {
            m.uiOwner.scannedShips.Remove(this);
            StartCoroutine(m.onDestroy());
        }

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

    //UI functions
    public void updateUI()
    {
        if(hasUI)
        {
            ui.setHullCounter(hpCurrent);
            ui.setSpeedCounter((int)speedCurrent);
            ui.setEnergyCounter((int)energyCurrent);
        }
    }

    //AI functions
    public void lookAt(Vector3 point)
    {
        float step = yawSpeed * 0.01f * Time.deltaTime;
        Quaternion rotQuat = Quaternion.LookRotation(point - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotQuat, step);
    }

    //customizer functions
    public void turnOnCustomize()
    {
        ui.gameObject.SetActive(false);
        markerUI.gameObject.SetActive(false);
        if(mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
        }
        overrideControls = true;
    }

    public void turnOffCustomize()
    {
        ui.gameObject.SetActive(true);
        markerUI.gameObject.SetActive(true);
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
        }
        overrideControls = false;
    }

    public Model toModel()
    {
        List<string> weponPrefabPaths = new List<string>();
        if(weapons.Count > 0)
        {
            foreach (Weapon w in weapons)
            {
                if(w != null)
                {
                    weponPrefabPaths.Add(w.prefabPath);
                }
            }
        }

        Model model = new ShipModel(prefabPath, displayName, weponPrefabPaths);

        return model;
    }

    void removeAllweapons()
    {
        for(int n = 0; n < weapons.Count; n++)
        {
            weapons[n].detachWeapon();
        }
    }

    public void fromModel(ShipModel importedModel)
    {
        for (int n = 0; n < weaponMounts.Length; n++)
        {
            if (weaponMounts[n].GetComponentInChildren<Weapon>() != null)
            {
                weaponMounts[n].GetComponentInChildren<Weapon>().detachWeapon();
            }
        }

        if (importedModel.weaponPrefabPaths.Count > 0)
        {
            for (int n = 0; n < weaponMounts.Length && n < importedModel.weaponPrefabPaths.Count; n++)
            {
                GameObject weapon = Instantiate(Resources.Load(importedModel.weaponPrefabPaths[n]) as GameObject, weaponMounts[n].transform);
                weapon.transform.position = weaponMounts[n].transform.position;
                weapon.transform.rotation = weaponMounts[n].transform.rotation;
                Weapon weaponComponent = weapon.GetComponent<Weapon>();
                weaponComponent.owner = this;
                weaponComponent.attachWeapon();
            }
        }
    }
}
