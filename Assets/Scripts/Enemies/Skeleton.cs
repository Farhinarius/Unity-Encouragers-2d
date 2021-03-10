using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float maxspeed = 35f;
    public float speed;
    public bool isTouch = false;

    Vector2 Fal;
    private void Start() 
    {
        speed = maxspeed;
    }
    
    private void FixedUpdate() 
    {
        float distance = Vector2.Distance(PlayerController.staticController.transform.position, transform.position);
        // angle between skeleton and mage
        
        if (distance <= 80) 
        {
            float angle = Random.Range(-0.5f, 0.5f) + Mathf.Atan2(PlayerController.staticController.transform.position.y - transform.position.y, PlayerController.staticController.transform.position.x - transform.position.x);
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed * 250);
        }
        else
        {
            float angle = Random.Range(0, Mathf.PI * 2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed; // move around
        }


        if (speed < maxspeed)
        {
            speed += Time.deltaTime * 100;
            if (speed > maxspeed)
            {
                speed = maxspeed;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if ( other.gameObject.CompareTag("Player") )
        {
            // Debug.Log("Skeleton Hit: " + other.gameObject);
            PlayerController.staticController.GetComponent<DamageablePlayer>().UpdateHealth(-35);
        }
    } 
}
