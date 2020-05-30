using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    public Transform owner; //TODO
    public int damage = 1;
    public int faction = 0;
    public float lifeTime = 2f;
    protected float lifeTimeRemaining = 2f;
    protected Ship ownerShip;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        lifeCycle();
    }

    //TODO
    protected virtual void init()
    {
        lifeTimeRemaining = lifeTime;
        Destroy(gameObject, lifeTime);
        if (owner != null)
        {
            ownerShip = owner.GetComponent<Ship>();
            if(ownerShip != null)
            {
                speed += ownerShip.speedCurrent;
            }
        }
    }

    protected virtual void lifeCycle()
    {
        moveForward();
        checkAlive();
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
                        onDestroy();
                        return;
                    }
                }
                other.GetComponent<IHitable>().onHit(damage);
                if (ownerShip != null)
                {
                    ownerShip.markerUI.spawnHitMarker();
                }
            }
            onDestroy();
            return;
        }
    }

    protected virtual void moveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    protected virtual void checkAlive()
    {
        if(lifeTimeRemaining > 0)
        {
            lifeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            onDestroy();
        }
    }

    protected virtual void onDestroy()
    {
        Destroy(gameObject);
    }
}
