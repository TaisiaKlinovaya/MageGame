using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class hud : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text manaText;
    public TMP_Text healthText;
    public TMP_Text lvl;

    public static int score = 0;
    public static int level = 0;


    void Start()
    {
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
        manaText.text = "Mana: " + MageScript.manaMage;
        lvl.text = "Level: " + level;

        //healthText.text = "Health: " + MageScript.health;
    }
}
