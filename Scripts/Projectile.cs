using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    public Transform owner; //TODO
    public int damage = 1;
    public int faction = 0;
    Ship ownerShip;

    // Start is called before the first frame update
    void Start()
    {
        init();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    //TODO
    protected virtual void init()
    {
        if(owner != null)
        {
            ownerShip = owner.GetComponent<Ship>();
            if(ownerShip != null)
            {
                speed += ownerShip.speedCurrent;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform != owner && other.tag != "NoProjectileTrigger")
        {
            if(other.GetComponent<IHitable>() != null)
            {

                if(other.GetComponent<Entity>() != null)
                {
                    if(faction == other.gameObject.GetComponent<Entity>().faction)
                    {
                        Destroy(gameObject);
                        return;
                    }
                }
                other.GetComponent<IHitable>().onHit(damage);
                if (ownerShip != null)
                {
                    ownerShip.markerUI.spawnHitMarker();
                }
            }
            Destroy(gameObject);
            return;
        }
    }


}
