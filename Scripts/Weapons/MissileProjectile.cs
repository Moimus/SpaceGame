using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : Projectile
{
    public Transform target;
    public float yawSpeed = 100f;
    public GameObject trail;
    public GameObject explosionFx;
    public float aimTime = 2f;
    float aimTimeRemaining = 2f;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        moveForward();
        followTarget();
    }

    void followTarget()
    {
        if(target != null)
        {
            if (aimTimeRemaining > 0)
            {
                float step = yawSpeed * 0.01f * Time.deltaTime;
                Quaternion rotQuat = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotQuat, step);
                aimTimeRemaining -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != owner && other.tag != "NoProjectileTrigger")
        {
            if (other.GetComponent<IHitable>() != null)
            {

                if (other.GetComponent<Entity>() != null)
                {
                    if (target != null && target.GetComponent<Entity>().lockingMissiles.Contains(this))
                    {
                        target.GetComponent<Entity>().lockingMissiles.Remove(this);
                    }

                    if (faction == other.gameObject.GetComponent<Entity>().faction)
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

            if(explosionFx != null)
            {
                GameObject explosionInstance = Instantiate(explosionFx, transform.position, transform.rotation);
            }

            trail.transform.parent = null;
            Destroy(trail.gameObject, 1f);
            onDestroy();
            return;
        }
    }
}
