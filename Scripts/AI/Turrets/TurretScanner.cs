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

    public void setTargetOffset()
    {
        owner.targetOffsetMax = owner.target.speedCurrent * 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.GetComponent<Entity>() != null)
            {
                Entity entity = other.GetComponent<Entity>();
                if (entity.faction != owner.faction && owner.target == null)
                {
                    owner.target = entity;
                    setTargetOffset();
                }
            }
        }
        catch(System.Exception)
        {

        }

    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            if (other != null)
            {
                if (other.GetComponent<Entity>() != null)
                {
                    if (other.GetComponent<Entity>() == owner.target)
                    {
                        owner.target = null;
                    }
                }
            }
            else
            {
                owner.target = null;
            }
        }
        catch(System.Exception)
        {

        }


    }
}
