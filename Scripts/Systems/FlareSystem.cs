using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareSystem : ShipSystem
{
    public GameObject[] flareSpawners;
    public GameObject flareProjectilePrefab;
    public Entity owner;
    public float fireDelay = 0.5f;
    float relativeSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void activate()
    {
        if(owner.lockingMissiles.Count > 0)
        {
            foreach(MissileProjectile m in owner.lockingMissiles)
            {
                m.target = null;
            }
        }
        StartCoroutine(spawnFlares());
    }

    IEnumerator spawnFlares()
    {
        for(int n = 0; n < 3; n++)
        {
            foreach(GameObject spawner in flareSpawners)
            {
                GameObject flare = Instantiate(flareProjectilePrefab);
                flare.transform.position = spawner.transform.position;
                flare.transform.rotation = spawner.transform.rotation;
            }
            yield return new WaitForSeconds(fireDelay);
        }
        yield return null;
    }
}
