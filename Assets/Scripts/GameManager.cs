using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static float timer = 30;
    public string state = "Start";
    public static GameManager manager;
    public GameObject pause;

    private void Start()
    {
        DontDestroyOnLoad(this);
        manager = this;
        
    }
    void Update()
    {
        /*
        //checks if Game is running and counts down timer. When time is up, resets timer and switches to start screen
        if(state == "Game" && timer > 0)
        {
            timer -= Time.deltaTime;
        } if (timer <= 0)
        {
            SceneManager.LoadScene("StartScreen");
            timer = 30;
            state = "Start";
        }

        // Pause Stand wird aktiviert wenn ESC gedrückt
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
        }
        */
    }

    public void onClickStart()
    {
        SceneManager.LoadScene("WizardMovement");
        state = "Game";

    }

    public void newGame()
    {
        playerStats.backup = null;
        SceneManager.LoadScene("WizardMovement");
        state = "Game";
    }

    public void exitGame()
    {
        Application.Quit();

    }
    public static void loadStart()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
