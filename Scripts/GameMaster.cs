using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public enum gameModes
    {
        sandBox = 0,
        deathMatch = 1
    }

    public List<Transform> respawnPoints;
    public int respawnTickets = 2;
    public int gameMode = 1;

    public Player player1;
    public Player player2;

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

    public void endGame()
    {
        if(gameMode == (int)gameModes.deathMatch)
        {
            StartCoroutine(GetComponent<DeathMatchUi>().endGame());
        }
    }

    public Player getOtherPlayer(Player p)
    {
        if(p.playerNo == 1)
        {
            return player2;
        }
        else if(p.playerNo == 2)
        {
            return player1;
        }
        else
        {
            return null;
        }
    }

}
