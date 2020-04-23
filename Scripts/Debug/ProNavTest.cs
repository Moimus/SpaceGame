using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProNavTest : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Vt=" + target.GetComponent<Rigidbody>().velocity.ToString() + "// Vm= " + GetComponent<Rigidbody>().velocity.ToString());
        //Debug.Log(Math.getProNavRotationVector(transform.position, target.position, GetComponent<Rigidbody>().velocity, target.GetComponent<Rigidbody>().velocity).ToString());
        transform.Translate(Vector3.forward * Time.deltaTime);
        transform.Rotate(Math.getProNavRotationVector(transform.position, target.position, -Vector3.forward, target.forward) * Time.deltaTime * 300);
    }
}
