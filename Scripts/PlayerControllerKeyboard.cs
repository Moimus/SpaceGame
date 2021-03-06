﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKeyboard : MonoBehaviour
{
    public Ship ship;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!ship.overrideControls)
        {
            checkMouseInput();
            checkKeyboardInput();
        }
    }



    void checkKeyboardInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ship.speedUp();
        }
        else
        {
            ship.slowDown();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            ship.rollLeft();
        }
        else if(Input.GetKey(KeyCode.E))
        {
            ship.rollRight();
        }
        else
        {
            ship.rollIdle();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            ship.scan();
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ship.toggleCruiseMode();
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            ship.setFOV(60);
        }
        else if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ship.setFOV(170);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ship.nextTarget();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            ship.activateSystem();
        }
    }

    void checkMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            ship.fireWeapons();
        }

        if (Input.GetMouseButton(1))
        {
            ship.followMouse();
        }
    }

}
