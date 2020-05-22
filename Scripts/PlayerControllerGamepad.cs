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
        if(!ship.overrideControls)
        {
            StartCoroutine(checkInput());
        }
    }

    IEnumerator checkInput()
    {
        //Left stick controls
        if (Input.GetAxis("P1LeftStickHorizontal") < -0.4f)
        {
            ship.yawLeft(-Input.GetAxis("P1LeftStickHorizontal"));
        }
        else if (Input.GetAxis("P1LeftStickHorizontal") > 0.4f)
        {
            ship.yawRight(Input.GetAxis("P1LeftStickHorizontal"));
        }

        if (Input.GetAxis("P1LeftStickVertical") > 0.4f)
        {
            ship.yawDown(Input.GetAxis("P1LeftStickVertical"));
        }
        else if (Input.GetAxis("P1LeftStickVertical") < -0.4f)
        {
            ship.yawUp(-Input.GetAxis("P1LeftStickVertical"));
        }

        //Right stick controls
        if (Input.GetAxis("P1RightStickHorizontal") < -0.8f)
        {
            //Debug.Log("right stick Horizontal Axis: " + Input.GetAxis("P1RightStickHorizontal"));
        }
        else if (Input.GetAxis("P1RightStickHorizontal") > 0.8f)
        {
            //Debug.Log("right stick Horizontal Axis: " + Input.GetAxis("P1RightStickHorizontal"));
        }

        if (Input.GetAxis("P1RightStickVertical") > 0.8f)
        {
            //Debug.Log("right stick Vertical Axis: " + Input.GetAxis("P1RightStickVertical"));
        }
        else if (Input.GetAxis("P1RightStickVertical") < -0.8f)
        {
            //Debug.Log("right stick Vertical Axis: " + Input.GetAxis("P1RightStickVertical"));
        }

        //Trigger controls
        if (Input.GetAxis("P1LeftTrigger") == 1)
        {
            //Debug.Log("left trigger Axis: " + Input.GetAxis("P1LeftTrigger"));
            ship.rollLeft();
        }
        else if (Input.GetAxis("P1RightTrigger") == 1)
        {
            //Debug.Log("right trigger Axis: " + Input.GetAxis("P1RightTrigger"));
            ship.rollRight();
        }
        else
        {
            ship.rollIdle();
        }

        //Button controls
        if (Input.GetButton("P1Abutton"))
        {
            //Debug.Log("A-Button down");
            ship.speedUp();
        }
        else if (Input.GetButton("P1Bbutton"))
        {
            //Debug.Log("B-Button down");
            ship.slowDown();
        }

        if (Input.GetButtonDown("P1Xbutton"))
        {
            //Debug.Log("X-Button down");
            ship.fireWeapons();
        }

        if (Input.GetButtonDown("P1Ybutton"))
        {
            //Debug.Log("Y-Button down");
            ship.scan();
        }

        if(Input.GetButtonDown("P1RBbutton"))
        {
            //Debug.Log("RB-Button down");
            ship.nextTarget();
        }

        yield return null;
    }
}
