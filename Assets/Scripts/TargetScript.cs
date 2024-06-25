using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

public class TargetScript : MonoBehaviour
{
    public int neededExp = 2;
    playerStats stats;
    public static int score = 0;
    public GameObject targetPrefab;
    public float timeSinceSpawn = 0;
    public float giveExp = 3;

    private void FixedUpdate()
    {
        // je länger der Ball fliegt, desto weniger EXP bekommt man
        timeSinceSpawn += Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        string tag = collision2D.gameObject.tag;

        if(tag == "Fireball")
        {
            Vector3 randomPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.nearClipPlane));
            Instantiate(targetPrefab, randomPosition, Quaternion.identity);

            Destroy(gameObject);
            score++;

            giveExp = giveExp / timeSinceSpawn + 1;

            MageScript player = MageScript.player;
            playerStats stats = player.stats;
            stats.GainXp(giveExp);

            timeSinceSpawn = 0;
        }
        
    }
}
