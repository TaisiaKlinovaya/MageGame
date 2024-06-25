using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class MageScript : MonoBehaviour
{
    public static MageScript player;

    public GameObject fireballPrefab;
    public float fireforce = 10;
    private Animator animator;
    public Vector3 lastMove = Vector3.zero;

    public playerStats stats;
    public float mana;
    public float health;
    public static float speed;
    public static float recharge = 0;
    float speedBoost = 1;

    public int lastDir = 0;

    void Start()
    {
        player = this;
        animator = GetComponent<Animator>();

        //stats = new playerStats();
        stats = playerStats.Instance;
        mana = stats.maxMana;
        health = stats.maxHealth;
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(8, 9);


        Vector3 movement = Vector3.zero;
        

        if (Input.GetKey("d"))
        {
            movement += Vector3.right;
            lastDir = 1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey("a"))
        {
            movement += Vector3.left;
            lastDir = 2;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey("s"))
        {
            movement += Vector3.down;
            lastDir = 3;
        }
        if (Input.GetKey("w"))
        {
            movement += Vector3.up;
            lastDir = 4;
        }

        speedBoost = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedBoost = 2.0f;
        }

            transform.position += movement.normalized * Time.deltaTime * stats.movementSpeed * speedBoost;


        if (movement.x > 0 || movement.y > 0 || movement.x < 0 || movement.y < 0)       //Walk Animation 
        {
            lastMove = movement;
            animator.SetBool("Walking", true);
        } else
        {
            animator.SetBool("Walking", false);
        }




        // Attacke, Cooldown und Mana

        recharge -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && recharge <= 0.0f && mana > 20)
        {
            recharge = stats.castRecharge;
            mana -= 20;

            GameObject newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

            if(lastMove != Vector3.zero)
            {
                newFireball.GetComponent<FireBallScript>().direction = lastMove;
            } else
            {
                newFireball.GetComponent <FireBallScript>().direction = Vector3.right;
            }

            animator.SetTrigger("Attack");
        }
        else
        {
            animator.ResetTrigger("Attack");
        }

        mana = mana + Time.deltaTime * stats.manaRegeneration;
        if (mana > stats.maxMana)
        {
            mana = stats.maxMana;
        }

        health = health + Time.deltaTime * stats.healthRegeneration;
        if(health > stats.maxHealth)
        {
            health = stats.maxHealth;
        }

    }
}