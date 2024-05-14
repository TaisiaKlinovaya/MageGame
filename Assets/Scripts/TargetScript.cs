using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

public class TargetScript : MonoBehaviour
{
    public int neededExp = 2;


    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        Destroy(gameObject);
        Destroy(collision.gameObject); 

        hud.score++;        //erhöht score points

        if (hud.score %  neededExp == 0 && hud.score != 0)
        {
            hud.level++;
            
            neededExp += 3;
        }
    }
    void Update()
    {
        
    }
}
