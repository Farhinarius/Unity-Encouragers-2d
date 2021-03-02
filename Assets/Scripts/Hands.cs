using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    private Vector2 mouseLookDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (RightHandController.IsCalm == false)
        {
            if (GetComponent<RightHandController>() != null)
                mouseLookDirection = GetComponent<RightHandController>().MouseLookDirection1;
            if (GetComponent<LeftHandController>() != null)
                mouseLookDirection = GetComponent<LeftHandController>().mouseLookDirection2;
            if (other.tag == "Enemy")
            {
                other.GetComponent<Skeleton>().speed = 0;
                other.GetComponent<Destructible>().Damage();
                Debug.Log("HIT!");
                other.GetComponent<Rigidbody2D>().AddForce(mouseLookDirection * 100, ForceMode2D.Impulse);
                Debug.DrawRay(other.GetComponent<Skeleton>().transform.position, mouseLookDirection, Color.blue);

            }
            else if (other.tag == "Item")
            {
                other.GetComponent<Destructible>().Damage();
                other.GetComponent<Rigidbody2D>().AddForce(Vector2.up*20000, ForceMode2D.Force);


            }
        }
    }
}






