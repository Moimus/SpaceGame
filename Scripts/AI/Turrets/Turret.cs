﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Entity
{
    public GameObject rotatorLeftRight;
    public GameObject rotatorUpDown;
    public Weapon[] weapons;
    public Entity target;
    public float rotationSpeed = 1;
    public float fireCooldown = 0.5f;
    private float remainingCooldown = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateLoop();
    }

    void stateLoop()
    {
        if(target != null)
        {
            lookAtTarget();
        }
        
        if (remainingCooldown > 0)
        {
            remainingCooldown -= Time.deltaTime;
        }
        else if(remainingCooldown <= 0 && target != null)
        {
            fireWeapons();
        }
    }

    void lookAtTarget()
    {
        try
        {
            lookAtTargetLeftRight();
            lookAtTargetUpDown();
        }
        catch(System.NullReferenceException)
        {
            target = null;
        }

    }

    void lookAtTargetLeftRight()
    {
        float step = rotationSpeed * Time.deltaTime;
        Quaternion rotQuat = Quaternion.LookRotation(target.transform.position - rotatorLeftRight.transform.position);
        Quaternion targetRotation = (Quaternion.Slerp(rotatorLeftRight.transform.rotation, rotQuat, step));
        Quaternion targetAngle = new Quaternion(rotatorLeftRight.transform.rotation.x, targetRotation.y, rotatorLeftRight.transform.rotation.z, rotatorLeftRight.transform.rotation.w);
        rotatorLeftRight.transform.rotation = targetAngle;
    }

    void lookAtTargetUpDown()
    {
        float step = rotationSpeed * Time.deltaTime;
        Quaternion rotQuat = Quaternion.LookRotation(target.transform.position - rotatorUpDown.transform.position);
        Quaternion targetRotation = (Quaternion.Slerp(rotatorUpDown.transform.rotation, rotQuat, step));
        Debug.Log(rotatorUpDown.transform.rotation.ToString());
        Quaternion targetAngle = new Quaternion(targetRotation.x ,0 ,0, rotatorUpDown.transform.rotation.w);
        rotatorUpDown.transform.localRotation = targetAngle;
    }

    void fireWeapons()
    {
        foreach(Weapon w in weapons)
        {
            w.fire();
        }
        remainingCooldown = fireCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}