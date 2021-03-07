using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WarriorController : MonoBehaviour
{
    private PlayerController player;
    private HandsController hands;
    private bool dashProcessing;
    private Vector3 dashStartPoint;
    private Vector2 dashEndPoint;
    private float dashTime;
    private Vector2 moveDirection;
    private Rigidbody2D rb2d;
    void Start()
    {
        player = GetComponent<PlayerController>();
        hands = GetComponent<HandsController>();
        rb2d = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        MovementDirection();
      
    }

    void FixedUpdate()
    {
        Dash(); 
    }

    private void MovementDirection()
    {
        moveDirection = (rb2d.position + player.playerLastMoveDirection) - rb2d.position;
    }
    private void Dash()
    {
        if ( ( Input.GetButtonDown("Fire2") ) && ( dashProcessing == false ) )
        {
            dashStartPoint = rb2d.position;
            Debug.Log("Dash");
            dashProcessing = true;
            dashTime = 0f;

        }
        if (dashProcessing)
        {
            rb2d.MovePosition(rb2d.position + moveDirection * Time.fixedDeltaTime * 120);
            dashTime += Time.fixedDeltaTime;
            if (dashTime >= 0.1f)
            {
                dashProcessing = false;
            }
            
        }
        
    }

}