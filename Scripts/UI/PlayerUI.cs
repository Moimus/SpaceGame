using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text speedCounter;
    public Text hullCounter;
    public Text energyCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSpeedCounter(int value)
    {
        speedCounter.text = (value * 10).ToString() + " au/h";
    }

    public void setHullCounter(int value)
    {
        hullCounter.text = value.ToString() + " HP";
    }

    public void setEnergyCounter(int value)
    {
        if(value > 0)
        {
            energyCounter.text = value.ToString() + " MW";
        }
        else
        {
            energyCounter.text = "0 MW";
        }
    }
}
