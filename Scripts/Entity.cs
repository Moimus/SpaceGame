using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Sheet : Entity")]
    public int faction = 0;
    public bool alive = true;
    public int hpMax = 10;
    public int hpCurrent;

    [Header("ControlVariables : Entitiy")]
    public float speedCurrent = 0f;

    [Header("ControlVariables : MissileReferences")]
    public List<MissileProjectile> lockingMissiles = new List<MissileProjectile>(); //list contains all missileprojectiles that are locked on this ship

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
