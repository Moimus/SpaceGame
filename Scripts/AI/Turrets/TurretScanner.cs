using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScanner : MonoBehaviour
{
    public Turret owner;

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
        if(other.GetComponent<Entity>() != null)
        {
            Entity entity = other.GetComponent<Entity>();
            if (entity.faction != owner.faction && owner.target == null)
            {
                owner.target = entity;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Entity>() != null)
        {
            if(other.GetComponent<Entity>() == owner.target)
            {
                owner.target = null;
            }
        }
    }
}
