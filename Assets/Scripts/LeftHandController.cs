using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandController : MonoBehaviour
{


//--------------------------------------------------------------------------------------
    public Transform PlayerH;
    private Vector3 HandRegularPosition;
    private Vector2 HandRegularPositionV2;
    private Vector3 HandAttackPosition;
    private Vector2 HandAttackPositionV2;

    private float destinationStart = 0f;
    private float destinationEnd = 0f;
    private bool LightAttacked = false;
    private bool HandBack = false;
//--------------------------------------------------------------------------------------



//--------------------------------------------------------------------------------------
    private Ray mouseRayCast;
    private Vector3 target;
    public Vector2 mouseLookDirection2;

//--------------------------------------------------------------------------------------






//--------------------------------------------------------------------------------------
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TransformPos();
        MouseLookDirection();
        Attack();

        LightAttack();
    }
//--------------------------------------------------------------------------------------








//--------------------------------------------------------------------------------------
    private void TransformPos()
    {   
        HandRegularPosition = new Vector3(PlayerH.position.x - 2.9f, PlayerH.position.y - 1.8f);
        HandRegularPositionV2 = HandRegularPosition;
        transform.position = HandRegularPosition;

        if (RightHandController.IsCalm == true)
        {
            destinationStart = 0f;
            destinationEnd = 0f;
        }
    }
//-------------------------------------------------------------------------------------- 



//--------------------------------------------------------------------------------------
    private void Attack()
    {
        if (Input.GetButton("Fire1")) 
        {
            RightHandController.IsCalm = false;
        }
    }
//--------------------------------------------------------------------------------------




//--------------------------------------------------------------------------------------
    private void MouseLookDirection()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseLookDirection2 = target - PlayerH.position;
        mouseRayCast = new Ray(PlayerH.position, mouseLookDirection2);
        HandAttackPosition = mouseRayCast.GetPoint(7);
    } 
//--------------------------------------------------------------------------------------



//--------------------------------------------------------------------------------------
    private void LightAttack()
    {
        if ( (RightHandController.IsCalm == false) && (RightHandController.OperateHand == 1) )
        {

            if ( (transform.position != HandAttackPosition) && (LightAttacked == false) )
            {
                transform.position = Vector2.MoveTowards(HandRegularPosition, HandAttackPosition, destinationStart);
                destinationStart += Time.deltaTime * 75;
            }


            if (transform.position == HandAttackPosition)
            {
                HandBack = true;
            }

            if (HandBack == true)        
            {
                LightAttacked = true;
                transform.position = Vector2.MoveTowards(HandAttackPosition, HandRegularPosition, destinationEnd);
                destinationEnd += Time.deltaTime * 75;
                
                if (transform.position == HandRegularPosition)
                {
                    RightHandController.IsCalm = true;
                    LightAttacked = false;
                    HandBack = false;
                    RightHandController.OperateHand = 0;
                }
            }
        }
    }
//--------------------------------------------------------------------------------------




















}