using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareProjectile : MonoBehaviour
{
    public float mass = 0.2f;
    public float rightSpeed = 2f;
    float rightSpeedCurrent = 2f;
    public float speedDecrease = 2f;
    public float lifeTime = 10f;
    float lifeTimeRemaining = 10f;
    public GameObject[] particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        rightSpeedCurrent = rightSpeed;
        lifeTimeRemaining = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeCycle();
        moveDown();
        moveForward();
        decreaseSpeed();
    }

    void moveDown()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * mass);
    }

    void moveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * rightSpeedCurrent);
    }

    void decreaseSpeed()
    {
        if(rightSpeedCurrent > 0)
        {
            rightSpeedCurrent -= Time.deltaTime * speedDecrease;
        }
        else if(rightSpeedCurrent < 0)
        {
            rightSpeedCurrent = 0;
        }
    }

    void lifeCycle()
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

    void onDestroy()
    {
        foreach(GameObject g in particleSystems)
        {
            g.transform.parent = null;
            Destroy(g, 3f);
        }
        Destroy(gameObject);
    }
}
