using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Ship ship;

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
   
            GameObject p = Instantiate(projectile);
            p.GetComponent<Projectile>().owner = gameObject.transform;
            p.transform.position = bulletSpawner.transform.position;
            p.transform.rotation. = bulletSpawner.transform.rotation;
            energyCurrent -= 10f;
        

    }

}
