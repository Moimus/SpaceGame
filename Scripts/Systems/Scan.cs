using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public float lifeTime = 3f;
    public float speed = 3f;
    public Ship owner;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        grow();
    }

    void grow()
    {
        float uniformScale = transform.localScale.x + (Time.deltaTime * speed);
        Vector3 scale = new Vector3(uniformScale, uniformScale, uniformScale);
        transform.localScale = scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Ship>() != null)
        {
            Ship otherShip = other.GetComponent<Ship>();
            if(owner.scannedShips.Contains(otherShip) == false)
            {
                owner.scannedShips.Add(otherShip);
                owner.markerUI.markTarget(otherShip);
            }
            Debug.Log("Ship scanned");
        }
    }
}
