using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Ship ship;
    public GameObject bulletSpawner;
    public GameObject projectile;
    public float energyConsumption = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void fire()
    {
        if(ship != null)
        {
            if (ship.energyCurrent > 0)
            {
                GameObject p = Instantiate(projectile);
                p.GetComponent<Projectile>().owner = ship.gameObject.transform;
                p.transform.position = bulletSpawner.transform.position;
                p.transform.rotation = bulletSpawner.transform.rotation;
                ship.energyCurrent -= energyConsumption;
            }
        }
        else
        {
            GameObject p = Instantiate(projectile);
            p.transform.position = bulletSpawner.transform.position;
            p.transform.rotation = bulletSpawner.transform.rotation;
        }
    }
    
}
