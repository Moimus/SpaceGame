using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Entity, IHitable, IDestructable
{
    public GameObject rotatorLeftRight;
    public GameObject rotatorUpDown;
    public TurretScanner scanner;
    public Weapon[] weapons;
    public Entity target;
    public float rotationSpeed = 1;
    public float fireCooldown = 0.5f;
    private float remainingFireCooldown = 0;
    public float targetOffsetInterval = 0.5f; //determines the interval to adapt the target trajectory
    private float targetOffsetRemaining = 0; //timer left
    public float targetOffsetUp = 0;
    public float targetOffsetForward = 0; //current total target trajectory offset
    public float targetOffsetStep = 1; //amount of trajectory adaption per step
    public float targetOffsetMax = 0; //maximum trajectory offset, set by TurretScanner (targetOffsetMax = target.speedCurrent * 1.5f)
    public GameObject explosionFx;

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
            calcTargetOffset();
        }
        
        if (remainingFireCooldown > 0)
        {
            remainingFireCooldown -= Time.deltaTime;
        }
        else if(remainingFireCooldown <= 0 && target != null)
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

    void calcTargetOffset()
    {
        scanner.setTargetOffset();
        float rand = Random.Range(-targetOffsetMax, targetOffsetMax);
        if (targetOffsetRemaining > 0)
        {
            targetOffsetRemaining -= Time.deltaTime;
        }
        else if (targetOffsetRemaining <= 0)
        {
            if (targetOffsetForward < targetOffsetMax)
            {
                targetOffsetForward += targetOffsetStep;
            }
            else if(targetOffsetForward > targetOffsetMax)
            {
                targetOffsetForward = 0;
            }

            if (targetOffsetUp < targetOffsetMax)
            {
                targetOffsetUp += targetOffsetStep;
            }
            else if (targetOffsetUp > targetOffsetMax)
            {
                targetOffsetUp = 0;
            }

            targetOffsetRemaining = targetOffsetInterval;
        }
    }

    void lookAtTargetLeftRight()
    {
        float step = rotationSpeed * Time.deltaTime;
        Vector3 trajectory = new Vector3(target.transform.position.x, target.transform.position.y , target.transform.position.z + targetOffsetForward); //actual point to aim at
        Quaternion rotQuat = Quaternion.LookRotation(trajectory - rotatorLeftRight.transform.position);
        Quaternion targetRotation = (Quaternion.Slerp(rotatorLeftRight.transform.rotation, rotQuat, step));
        Quaternion targetAngle = new Quaternion(rotatorLeftRight.transform.rotation.x, targetRotation.y, rotatorLeftRight.transform.rotation.z, rotatorLeftRight.transform.rotation.w);
        rotatorLeftRight.transform.rotation = targetAngle;
    }

    void lookAtTargetUpDown()
    {
        float step = rotationSpeed * Time.deltaTime;
        Vector3 trajectory = new Vector3(target.transform.position.x, target.transform.position.y + targetOffsetUp, target.transform.position.z); //actual point to aim at
        Quaternion rotQuat = Quaternion.LookRotation(target.transform.position - rotatorUpDown.transform.position);
        Quaternion targetRotation = (Quaternion.Slerp(rotatorUpDown.transform.rotation, rotQuat, step));
        //Debug.Log(rotatorUpDown.transform.rotation.ToString());
        Quaternion targetAngle = new Quaternion(targetRotation.x ,0 ,0, rotatorUpDown.transform.rotation.w);
        rotatorUpDown.transform.localRotation = targetAngle;
    }

    void fireWeapons()
    {
        foreach(Weapon w in weapons)
        {
            w.fire();
        }
        remainingFireCooldown = fireCooldown;

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void onHit(int damage)
    {
        hpCurrent -= damage;
        checkAlive();
    }

    public void onHit()
    {
        throw new System.NotImplementedException();
    }

    public void onDestroy()
    {
        GameObject explosion = Instantiate(explosionFx, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void checkAlive()
    {
        if (hpCurrent <= 0)
        {
            if (alive)
            {
                alive = false;
                onDestroy();
            }
        }
    }
}
