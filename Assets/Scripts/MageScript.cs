using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class MageScript : MonoBehaviour
{
    public static MageScript player;

    public GameObject fireballPrefab;
    private float attackCoolDownTime = 0.0f;
    public float fireforce = 10;
    private Animator animator;

    public playerStats stats;
    public static float manaMage;
    public static float healthMage;

    public int lastDir = 0;

    void Start()
    {
        player = this;
        animator = GetComponent<Animator>();

        stats = new playerStats();
        manaMage = stats.maxMana;
        healthMage = stats.maxHealth;
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(8, 9);


        Vector3 movement = Vector3.zero;
        Vector3 fireDirection = Vector3.zero;
        

        if (Input.GetKey("d"))
        {
            movement += Vector3.right * Time.deltaTime * stats.movementSpeed;
            lastDir = 1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey("a"))
        {
            movement += Vector3.left * Time.deltaTime * stats.movementSpeed;       //WASD controls
            lastDir = 2;
            //transform.rotation = Quaternion.Euler(0, -180, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey("s"))
        {
            movement += Vector3.down * Time.deltaTime * stats.movementSpeed;
            lastDir = 3;
        }
        if (Input.GetKey("w"))
        {
            movement += Vector3.up * Time.deltaTime * stats.movementSpeed;
            lastDir = 4;

        }


        if (movement.x > 0 || movement.y > 0 || movement.x < 0 || movement.y < 0)       //Walk Animation 
        {
            animator.SetBool("Walking", true);
        } else
        {
            animator.SetBool("Walking", false);
        }


        /*if (Input.GetKey(KeyCode.LeftShift))        //Shift für Sprint funktionieert nicht
        {
            movementSpeed = 20;
        }*/


        // Attacke und Cooldown
        if (Input.GetKeyDown(KeyCode.Space) && attackCoolDownTime <= 1.0f && manaMage > 20)
        {
            //ungefähr 2 Sekunden Cooldown
            attackCoolDownTime += 1.0f;


            //Mage Mana - 20 wenn geschossen
            manaMage -= 20;

            GameObject newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            newFireball.GetComponent<FireBallScript>().direction = movement;


            // Wenn Space gedrückt wird, geht der Ball in die Richtung vom Magier oder letzte gegangene Richtung
            if(movement != null)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(movement.normalized * fireforce, ForceMode2D.Impulse);
            } 

            //ohne Wizard Bewgung
            if (movement == Vector3.zero && lastDir == 1)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(300, 0, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);
            }
            if(movement == Vector3.zero && lastDir == 2)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(-300, 0, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);
            }
            if (movement == Vector3.zero && lastDir == 3)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -300, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);

            }
            if (movement == Vector3.zero && lastDir == 4)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 300, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);

            }
            if (movement == Vector3.zero && lastDir == 0)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(300, 0, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);
            }
        }
        else if (attackCoolDownTime > 1.0f)
        {
            attackCoolDownTime -= 1.0f * Time.deltaTime;        //Attack Cooldown rückzug
        } else if(manaMage < 100)
        {
            manaMage += stats.manaRegeneration;
        }




        //normalisierte Movements für Diagonale Bewegung
        transform.position += movement.normalized * Time.deltaTime * stats.movementSpeed;

    }
}