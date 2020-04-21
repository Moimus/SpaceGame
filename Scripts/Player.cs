using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject ship;
    public bool gamepadControl = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator startRespawnCounter()
    {
        Debug.Log("respawn!");
        //yield return new WaitForSeconds(5);
        GameObject newShip = Instantiate(ship, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        newShip.GetComponent<Ship>().player = this;
        yield return null;
    }
}
