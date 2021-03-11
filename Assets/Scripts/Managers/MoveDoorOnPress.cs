using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoorOnPress : MonoBehaviour
{
    public Transform AffectedObject;
    public bool returnDoorOnRelease;
    public float speed = 20f;
    private bool pressurePlatePressed;
    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = AffectedObject.position;
        AffectedObject.position = Vector2.MoveTowards(AffectedObject.position, AffectedObject.position + Vector3.left * 20, speed * Time.fixedDeltaTime);
    }


    private void FixedUpdate()
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(1)) SetPressurePlateState(true);
        else SetPressurePlateState(false);

        if (pressurePlatePressed)
        {
            if (transform.localScale.x > transform.localScale.y)
                AffectedObject.position = Vector2.MoveTowards(AffectedObject.position, AffectedObject.position + Vector3.left * 20 , speed * Time.fixedDeltaTime);
            else
                AffectedObject.position = Vector2.MoveTowards(AffectedObject.position, AffectedObject.position + Vector3.down * 20, speed * Time.fixedDeltaTime);
        }
        else
        {
            // close path
            if (returnDoorOnRelease)
            {
                AffectedObject.position = Vector2.MoveTowards(AffectedObject.position, startPosition, speed * Time.fixedDeltaTime);
            }
        }
    }

    private void SetPressurePlateState(bool newState)
    {
        if (pressurePlatePressed != newState)
        {
            pressurePlatePressed = newState;

            if (pressurePlatePressed)
            {
                transform.Find("Surface").localPosition = new Vector2(0, 0);
            }
            else
                transform.Find("Surface").localPosition = new Vector2(0, 0.05f);
        }
    }
}
