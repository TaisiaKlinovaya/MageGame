using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireBallScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        string tag = collision2D.gameObject.tag;

        if (tag == "Target")
        {
            Destroy(gameObject);
        }

    }

    public Rigidbody2D rb;
    public Vector3 direction;
    public GameObject TargetScript;
    private float angle;


    void Start()
    {

        
        angle = Vector3.Angle(direction, Vector3.right);

        if (direction.y < 0)
        {
            angle = angle * -1;
        }

        transform.Rotate(new Vector3(0, 0, angle));

    }
    void Update()
    {
        transform.position = transform.position + direction * 15 * Time.deltaTime;

        if (!IsVisibleOnScreen(gameObject))
        {
            Destroy(gameObject);
        }


        //Methode zur ¸berpr¸fung der Fire Ball Position
        bool IsVisibleOnScreen(GameObject target)
        {

            Camera mainCam = Camera.main;
            Vector3 targetScreenPoint = mainCam.WorldToScreenPoint(target.GetComponent<Renderer>().bounds.center);

            if ((targetScreenPoint.x < 0) || (targetScreenPoint.x > Screen.width) ||
                (targetScreenPoint.y < 0) || (targetScreenPoint.y > Screen.height))     //ist Ball auﬂerhalb der Cam View?
            {
                return false;
            }
            if (targetScreenPoint.z < 0)
            {
                return false;
            }
            return true;
        }
    }

}
