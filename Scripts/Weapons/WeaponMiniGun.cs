using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMiniGun : Weapon
{
    /// <summary>
    /// How long it takes before the gun starts firing
    /// </summary>
    public float spinCounterTarget = 1f;
    /// <summary>
    /// How is the gun spinning at the moment
    /// </summary>
    float spinCounterCurrent = 0f;
    /// <summary>
    /// how accurate the gun is in degrees
    /// </summary>
    public float spread = 5f;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        weaponLoop();
        if(owner.isPlayer)
        {
            getInput();
        }
        else
        {

        }
    }

    /// <summary>
    /// instantiate the projectile and set its position, owner etc.
    /// </summary>
    public override void fire()
    {
        if (active && cooldownRemaining <= 0 && spinCounterCurrent >= spinCounterTarget)
        {
            Ship ship;
            ship = owner as Ship;
            if (owner != null)
            {
                if (ship != null && ship.energyCurrent > 0)
                {
                    GameObject p = Instantiate(projectile);
                    p.GetComponent<Projectile>().owner = ship.gameObject.transform;
                    p.GetComponent<Projectile>().faction = owner.faction;
                    p.transform.position = bulletSpawner.transform.position;
                    float randomAngleX = Random.Range(-5, 5);
                    float randomAngleY = Random.Range(-5, 5);
                    float randomAngleZ = Random.Range(-5, 5);
                    p.transform.rotation = Quaternion.Euler(bulletSpawner.transform.rotation.eulerAngles.x + randomAngleX, bulletSpawner.transform.rotation.eulerAngles.y + randomAngleY, bulletSpawner.transform.rotation.eulerAngles.z + randomAngleZ);
                    cooldownRemaining = cooldown;
                    if (ship != null)
                    {
                        ship.energyCurrent -= energyConsumption;
                    }
                }
                else if (ship == null)
                {
                    GameObject p = Instantiate(projectile);
                    p.GetComponent<Projectile>().owner = owner.gameObject.transform;
                    p.GetComponent<Projectile>().faction = owner.faction;
                    p.transform.position = bulletSpawner.transform.position;
                    float randomAngleX = Random.Range(-5, 5);
                    float randomAngleY = Random.Range(-5, 5);
                    float randomAngleZ = Random.Range(-5, 5);
                    p.transform.rotation = Quaternion.Euler(bulletSpawner.transform.rotation.eulerAngles.x + randomAngleX, bulletSpawner.transform.rotation.eulerAngles.y + randomAngleY, bulletSpawner.transform.rotation.eulerAngles.z + randomAngleZ);
                    cooldownRemaining = cooldown;
                }
            }
            else
            {
                GameObject p = Instantiate(projectile);
                p.transform.position = bulletSpawner.transform.position;
                p.transform.rotation = bulletSpawner.transform.rotation;
                cooldownRemaining = cooldown;
            }
        }
        else if (active && spinCounterCurrent <= spinCounterTarget)
        {
            spinCounterCurrent += Time.deltaTime;
        }
        
        if(animator.GetCurrentAnimatorClipInfo(0).Length > 0 && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "fire")
        {
            animator.Play("fire", 0);
        }

    }

    /// <summary>
    /// reset the spinning time once left mouseButton or Gamepad X-Button is released
    /// </summary>
    void getInput()
    {
        if ( (Input.GetMouseButtonUp(0) || Input.GetButtonUp("P1Xbutton")) && spinCounterCurrent > 0)
        {
            releaseSpin();
        }
    }

    /// <summary>
    /// manually reset spin time to zero
    /// </summary>
    public void releaseSpin()
    {
        spinCounterCurrent = 0;
    }

    public override void releaseFire()
    {
        base.releaseFire();
        releaseSpin();
    }
}
