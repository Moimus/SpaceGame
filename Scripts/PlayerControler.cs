
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Ship ship;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkMouseInput();
        checkKeyboardInput();
        ship.stateLoop();
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
    }

    void checkMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ship.fire();
        }

        if (Input.GetMouseButton(1))
        {
            ship.lookAtMouse();
        }
    }

}
