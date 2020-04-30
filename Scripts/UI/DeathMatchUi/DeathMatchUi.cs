using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMatchUi : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public GameObject messagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showWinMessage(Player winner, Player loser)
    {
        GameObject m = Instantiate(messagePrefab, winner.ship.markerUI.canvas.transform);
        m.GetComponent<Message>().setText("winner");
        m.GetComponent<Message>().setColor(0f, 255f, 0f, 255f);

        GameObject n = Instantiate(messagePrefab, loser.ship.markerUI.canvas.transform);
        n.GetComponent<Message>().setText("loser");
        n.GetComponent<Message>().setColor(255f, 0, 0f, 255f);
        loser.GetComponent<Player>().ship.markerUI.transform.parent = null;
        Destroy(loser.GetComponent<Player>().ship.markerUI.crossHair);
    }

    public IEnumerator endGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        yield return null;
    }
}
