using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public List<Transform> respawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getRandomRespawnPosition()
    {
        int rand = Random.Range(0, respawnPoints.Count);
        try
        {
            return respawnPoints[rand].position;
        }
        catch(System.ArgumentOutOfRangeException are)
        {
            return new Vector3(0, 0, 0);
        }
    }

}
