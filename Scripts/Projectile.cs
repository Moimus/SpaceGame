using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    public Transform owner; //TODO
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
        ownerShip = owner.GetComponent<Ship>();
        speed += ownerShip.speedCurrent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform != owner)
        {
            if(other.GetComponent<IHitable>() != null)
            {
                other.GetComponent<IHitable>().onHit();
            }
            Destroy(gameObject);
        }
    }


}
