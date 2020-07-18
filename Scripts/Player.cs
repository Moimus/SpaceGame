using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameMaster gameMaster;
    public int playerNo;
    public GameObject shipPrefab;
    public Ship ship;
    public bool gamepadControl = false;
    public int respawnTickets;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void init()
    {
        respawnTickets = gameMaster.respawnTickets;
    }

    public void startRespawnCounter()
    {
        if (respawnTickets > 0)
        {
            StartCoroutine(respawnCounter());
        }
        else if(gameMaster.respawnTickets == -1)
        {
            StartCoroutine(respawnCounter());
        }
        else
        {
            if(gameMaster.gameMode == (int)GameMaster.gameModes.deathMatch)
            {
                gameMaster.GetComponent<DeathMatchUi>().showWinMessage(gameMaster.getOtherPlayer(this), this);
                gameMaster.endGame();
            }
        }
    }

    //TODO load shipBlueprint instead of prefab
    public IEnumerator respawnCounter()
    {
        Debug.Log("respawn!");
        yield return new WaitForSeconds(5);
        Vector3 respawnPosition = gameMaster.getRandomRespawnPosition();
        GameObject newShip = Instantiate(shipPrefab, respawnPosition, new Quaternion(0, 0, 0, 0));
        ship = newShip.GetComponent<Ship>();
        newShip.GetComponent<Ship>().player = this;
        if(respawnTickets > 0)
        {
            respawnTickets -= 1;
        }
        yield return null;
    }
}
