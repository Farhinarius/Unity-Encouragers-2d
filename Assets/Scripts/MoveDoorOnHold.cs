using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoorOnHold : MonoBehaviour
{
    public Transform AffectedObject;
    public bool returnDoorOnRelease;
    public Vector3 endPosition;
    public float speed = 20f;
    
    private Vector3 startPosition;
    private bool pressurePlatePressed;

    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = AffectedObject.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() 
    {
        if ( GetComponent<Collider2D>().IsTouchingLayers(1) ) SetPressurePlateState(true);
        else SetPressurePlateState(false); 

        if (pressurePlatePressed)
        {
            // open path
            AffectedObject.position = Vector2.MoveTowards(AffectedObject.position, endPosition, speed * Time.fixedDeltaTime);
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
