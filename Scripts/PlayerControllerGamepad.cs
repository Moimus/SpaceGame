using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGamepad : MonoBehaviour
{
    public Ship ship;

    // Start is called before the first frame update
    void Start()
    {
        string[] n = Input.GetJoystickNames();
        foreach(string i in n)
        {
            Debug.Log(i.ToString());
        }

    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
    }

    void checkInput()
    {
        //Left stick controls
        if(Input.GetAxis("P1LeftStickHorizontal") != 0)
        {
            //Debug.Log("Left stick Horizontal Axis: " + Input.GetAxis("P1LeftStickHorizontal"));
        }

        if (Input.GetAxis("P1LeftStickVertical") != 0)
        {
            //Debug.Log("Left stick Vertical Axis: " + Input.GetAxis("P1LeftStickVertical"));
        }

        //Right stick controls
        if (Input.GetAxis("P1RightStickHorizontal") != 0)
        {
            //Debug.Log("right stick Horizontal Axis: " + Input.GetAxis("P1RightStickHorizontal"));
        }

        if (Input.GetAxis("P1RightStickVertical") != 0)
        {
            //Debug.Log("right stick Vertical Axis: " + Input.GetAxis("P1RightStickVertical"));
        }

        //Trigger controls
        if (Input.GetAxis("P1LeftTrigger") != 0)
        {
            Debug.Log("left trigger Axis: " + Input.GetAxis("P1LeftTrigger"));
        }

        if (Input.GetAxis("P1RightTrigger") != 0)
        {
            Debug.Log("right trigger Axis: " + Input.GetAxis("P1RightTrigger"));
        }

        //Button controls
        if (Input.GetButtonDown("P1Abutton"))
        {
            Debug.Log("A-Button down");
        }

        if (Input.GetButtonDown("P1Xbutton"))
        {
            Debug.Log("X-Button down");
        }

        if (Input.GetButtonDown("P1Bbutton"))
        {
            Debug.Log("B-Button down");
        }

        if (Input.GetButtonDown("P1Ybutton"))
        {
            Debug.Log("Y-Button down");
        }
    }
}
