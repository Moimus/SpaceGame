using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string deathMatchmap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadDeathMatch()
    {
        SceneManager.LoadScene(deathMatchmap, LoadSceneMode.Single);
    }
}
