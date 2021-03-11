using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Physics and positioning in the world script for player
public class PlayerController : MonoBehaviour
{
    public static PlayerController staticController;

    public float speed;
    public float defaultSpeed;
    public float speedModifier;
    Rigidbody2D rigidbody2d;
    private Vector2 lookDirection;
    private Vector2 movement;
    private Vector2 mouseTarget;
    public Vector2 interactiveRayLength = new Vector2(1.5f, 1.5f);
    public Vector2 LookDirection
    {
        get { return lookDirection; }
    }

    public bool needDash = false;
    public int defaultDamage;
    public int damageModifier;
    public int damage;

    private void Awake() {
        staticController = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
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

        damage = defaultDamage * damageModifier;
    }

    private void FixedUpdate()
    {
        MovementControl();
        if (needDash)
            Dash();
    }

    private void GetMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }
    
    private void MovementControl()
    {
        Vector2 positionToMove = rigidbody2d.position;
        positionToMove += movement * defaultSpeed * speedModifier * Time.fixedDeltaTime;
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

    // Warrior only
    public void Dash()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + movement * Time.fixedDeltaTime * 120);
    }

}