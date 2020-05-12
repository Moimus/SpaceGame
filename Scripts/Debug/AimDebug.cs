using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDebug : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position);
        transform.Translate(Vector3.forward * Time.deltaTime * 5);
    }
}
