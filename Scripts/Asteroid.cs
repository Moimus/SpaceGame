using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IHitable, IDestructable
{
    public GameObject explosionFx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHit()
    {
        onDestroy();
    }

    public void onDestroy()
    {
        GameObject explosion = Instantiate(explosionFx, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void onHit(int damage)
    {
        onDestroy();
    }

    public void checkAlive()
    {
        throw new System.NotImplementedException();
    }
}
