using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireBallScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 randomPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.nearClipPlane));
        Instantiate(TargetScript, randomPosition, Quaternion.identity);
        
    }

    public Rigidbody2D rb;
    public Vector3 direction;
    public GameObject TargetScript;
    private float angle;


    void Start()
    {

        //übernimmt den Angle basierend auf movement von Mage
        //TODO: richtige Richtung auch im stehen
        
        angle = Vector3.Angle(direction, Vector3.right);

        if (direction.y > 0)
        {
            transform.Rotate(new Vector3(0, 0, angle));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, (-1 * angle)));
        }

    }
    void Update()
    {

        if (!IsVisibleOnScreen(gameObject))
        {
            Destroy(gameObject);        //zerstörug des Balle

            //nach 3 Sekunden Obkekt zerstören
            //Destroy(GameObject, 3);
        }


        //Methode zur überprüfung der Fire Ball Position
        bool IsVisibleOnScreen(GameObject target)
        {

            Camera mainCam = Camera.main;
            Vector3 targetScreenPoint = mainCam.WorldToScreenPoint(target.GetComponent<Renderer>().bounds.center);

            if ((targetScreenPoint.x < 0) || (targetScreenPoint.x > Screen.width) ||
                (targetScreenPoint.y < 0) || (targetScreenPoint.y > Screen.height))     //ist Ball außerhalb der Cam View?
            {
                return false;
            }
            if (targetScreenPoint.z < 0)
            {
                return false;
            }
            return true;

            /* Version 2 für Ball zerstörung
             * 
             * float timer
             * timer = timer * Time.deltaTime;
             * 
             * if(timer <= 0){
             *      Destroy(gameObject);
              }
             */
        }
    }

}
