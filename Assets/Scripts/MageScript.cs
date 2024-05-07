using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MageScript : MonoBehaviour
{

    public GameObject fireballPrefab;
    public float movementSpeed = 2;
    private float attackCoolDownTime = 0.0f;
    public float fireforce;


    private int lastDir = 0;

    void Start()
    {
    
    }

    void Update()
    {

        /*
        //Der Wizard kann mit Tastendruck in verschiedene Richtungen gehen
        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= new Vector3(3, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= new Vector3(0, 3, 0) * Time.deltaTime;
        }
        if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0, 3, 0) * Time.deltaTime;
        } */


        //Variante 2
        Vector3 movement = Vector3.zero;
        Vector3 fireDirection = Vector3.zero;
        

        if (Input.GetKey("d"))
        {
            movement += Vector3.right * Time.deltaTime;

            lastDir = 1;
        }
        if (Input.GetKey("a"))
        {
            movement += Vector3.left * Time.deltaTime;       //WASD controls
            lastDir = 2;
        }
        if (Input.GetKey("s"))
        {
            movement += Vector3.down * Time.deltaTime;
            lastDir = 3;
        }
        if (Input.GetKey("w"))
        {
            movement += Vector3.up * Time.deltaTime;
            lastDir = 4;

        }


        if (Input.GetKey(KeyCode.LeftShift))        //Shift für Sprint
        {
            movementSpeed = 5;
        }


        // Attacke und Cooldown
        if (Input.GetKeyDown(KeyCode.Space) && attackCoolDownTime <= 1.0f)
        {      
            attackCoolDownTime += 2.0f;             //ungefähr 4 Sekunden Cooldown

            GameObject newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            newFireball.GetComponent<FireBallScript>().direction = movement;


            // Wenn Space gedrückt wird, geht der Ball in die Richtung vom Magier oder letzte gegangene Richtung
           /* if(movement != null)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(movement.normalized * fireforce, ForceMode2D.Impulse);
            } */

            //ohne Wizard Bewgung
            if (movement == Vector3.zero && lastDir == 1)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(100, 0, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);
            }
            if(movement == Vector3.zero && lastDir == 2)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(-100, 0, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);
            }
            if (movement == Vector3.zero && lastDir == 3)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -100, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);

            }
            if (movement == Vector3.zero && lastDir == 4)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 100, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);

            }
            if (movement == Vector3.zero && lastDir == 0)
            {
                newFireball.GetComponent<Rigidbody2D>().AddForce(new Vector3(100, 0, 0) * Time.deltaTime * fireforce, ForceMode2D.Impulse);
            }


            /*if(movement == Vector3.zero)
            {
                UnityEngine.Debug.Log("it is zero");
                UnityEngine.Debug.Log(movement);
            } */
        }
        else if (attackCoolDownTime > 1.0f)
        {
            attackCoolDownTime -= 1.0f * Time.deltaTime;
        }


        //normalisierte Movements für Diagonale Bewegung
        transform.position += movement.normalized * Time.deltaTime * movementSpeed;

    }
}