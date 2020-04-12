using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour, ICollectable, IDestructable
{
    public int healAmount = 2;
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
        if(other.GetComponent<Ship>() != null)
        {
            onCollect(other.GetComponent<Ship>());
        }
    }

    public void onCollect(Ship collector)
    {
        if(collector.hpCurrent < collector.hpMax)
        {
            collector.hpCurrent += healAmount;
            if(collector.hpCurrent > collector.hpMax)
            {
                collector.hpCurrent = collector.hpMax;
            }
            onDestroy();
        }
    }

    public void onDestroy()
    {
        GameObject explosion = Instantiate(explosionFx, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
