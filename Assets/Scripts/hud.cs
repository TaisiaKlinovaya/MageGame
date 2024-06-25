using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class hud : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text manaText;
    public TMP_Text healthText;
    public TMP_Text lvl;
    public TMP_Text EXP;
    public TMP_Text timer;

    public Image manaLevel;
    public Image healthLevel;


    void Update()
    {
        MageScript w = MageScript.player;
        playerStats s = w.stats;
        int maxHP = s.maxHealth;
        float ManaScale = (float)w.mana / s.maxMana;
        float HealthScale = (float)w.health / s.maxHealth;
        float timer1 = GameManager.timer;


        scoreText.text = "Score: " + TargetScript.score;
        healthText.text = "" + (int)w.health + "/" + maxHP;
        manaText.text = "" + (int)w.mana + "/" + (s.maxMana);
        lvl.text = "Level: " + s.level;
        EXP.text = "EXP: " + Mathf.Round(s.exp) + "/" + (s.level * 10);
        timer.text = "" + Mathf.Round(timer1);

        manaLevel.transform.localScale = new Vector3(ManaScale, 1, 1);
        healthLevel.transform.localScale = new Vector3(HealthScale, 1, 1);
    }
}
