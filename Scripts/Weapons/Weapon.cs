using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Entity owner;
    public GameObject bulletSpawner;
    public GameObject projectile;
    public float energyConsumption = 5f;
    public float cooldown = 0.5f;
    float cooldownRemaining = 0f;
    public bool active = false;

    [Header("DataModel")]
    public string prefabPath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weaponLoop();
    }

    public virtual void init(Entity owner)
    {
        this.owner = owner;
    }

    void weaponLoop()
    {
        if(cooldownRemaining > 0)
        {
            cooldownRemaining -= Time.deltaTime;
        }
    }

    public virtual void fire()
    {
        if(active && cooldownRemaining <= 0)
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
                    p.transform.rotation = bulletSpawner.transform.rotation;
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
                    p.transform.rotation = bulletSpawner.transform.rotation;
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
    }

    public virtual void detachWeapon()
    {
        Ship ship = owner as Ship;
        ship.weapons.Remove(this);
        Destroy(gameObject);
    }

    public virtual void attachWeapon()
    {
        Ship ship = owner as Ship;
        ship.weapons.Add(this);
    }


}
