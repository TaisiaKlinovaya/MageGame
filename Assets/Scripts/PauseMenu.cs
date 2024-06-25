using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class PauseMenu : MonoBehaviour
{
    public GameObject pause;

    private void Start()
    {
        GameManager.manager.pause = gameObject;
        gameObject.SetActive(false);
    }

    public void onClickStart()
    {
        gameObject.SetActive(false);
    }

    public void backToMenu()
    {
        GameManager.loadStart();
    }

}
