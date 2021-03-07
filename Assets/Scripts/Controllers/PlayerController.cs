using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Physics and positioning in the world script for player
public class PlayerController : MonoBehaviour
{
    public static PlayerController staticController;

    public float speed;
    Rigidbody2D rigidbody2d;
    private Vector2 lookDirection;
    private Vector2 movement;
    private Vector2 mouseTarget;
    public Vector2 interactiveRayLength = new Vector2(1.5f, 1.5f);
    public Vector2 LookDirection
    {
        get { return lookDirection; }
    }
    public bool MovingAvaible;
    public Vector2 playerLastMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        staticController = this;
        MovingAvaible = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        GetMouseInput();
        SetLookDirection();

        Debug.DrawLine(transform.position, mouseTarget, Color.red);
        //Debug.DrawRay(transform.position, lookDirection * 10, Color.blue);
        //Debug.Log("Get look direction: " + GetLookDirecton());
    }

    private void FixedUpdate() 
    {
        MovementControl();
    }

    private void GetMovementInput()
    {

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        playerLastMoveDirection = movement;
    }
    private void MovementControl()
    {
        Vector2 positionToMove = rigidbody2d.position;
        positionToMove += movement * speed * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(positionToMove);
    }

    private void GetMouseInput()
    {
        mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void SetLookDirection()
    {
        lookDirection = mouseTarget - rigidbody2d.position;
        lookDirection.Normalize();
        
        // angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
    }
}

