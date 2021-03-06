﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPowerUp : MonoBehaviour, ICollectable, IDestructable
{
    public int energyAmount = 100;
    public GameObject explosionFx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ship>() != null)
        {
            onCollect(other.GetComponent<Ship>());
        }
    }

    public void onCollect(Ship collector)
    {
        if (collector.energyCurrent < collector.energyMax)
        {
            collector.energyCurrent += energyAmount;
            if (collector.energyCurrent > collector.energyMax)
            {
                collector.energyCurrent = collector.energyMax;
            }
            onDestroy();
        }
    }

    public void onDestroy()
    {
        GameObject explosion = Instantiate(explosionFx, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void checkAlive()
    {
        throw new System.NotImplementedException();
    }
}
