using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireBallScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public Vector3 direction;

    void Update()
    {

        transform.position += direction * 4 * Time.deltaTime;       //Fire Ball movement

        if (!IsVisibleOnScreen(gameObject))
        {
            Destroy(gameObject);        //zerst�rug des Balle

            //nach 3 Sekunden Obkekt zerst�ren
            //Destroy(GameObject, 3);
        }


        //Methode zur �berpr�fung der Fire Ball Position
        bool IsVisibleOnScreen(GameObject target)
        {

            Camera mainCam = Camera.main;
            Vector3 targetScreenPoint = mainCam.WorldToScreenPoint(target.GetComponent<Renderer>().bounds.center);

            /* WorldToScreenPoint wandelt Game Koordinate in Bildschirmkoordinate um.
             * target.GetComponent<Renderer> erlaubt uns auf die bounds zu zugreifen. 
             * Bounds ist die rechteckige "Grenze" des GameObjects. 
             * Mit center wird die Mitte der bounds wiedergegeben */

            if ((targetScreenPoint.x < 0) || (targetScreenPoint.x > Screen.width) ||
                (targetScreenPoint.y < 0) || (targetScreenPoint.y > Screen.height))     //ist Ball au�erhalb der Cam View?
            {
                return false;
            }
            if (targetScreenPoint.z < 0)
            {
                return false;
            }
            return true;

            /* Version 2 f�r Ball zerst�rung
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
