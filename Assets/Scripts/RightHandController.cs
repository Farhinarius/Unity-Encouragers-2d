using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandController : MonoBehaviour
{


//--------------------------------------------------------------------------------------
    public Transform PlayerT;
    static public int OperateHand = 0;
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
    private Vector2 mouseLookDirection1;
    static public bool IsCalm = true;

    public Vector2 MouseLookDirection1 { get => mouseLookDirection1; private set => mouseLookDirection1 = value; }

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
        HandRegularPosition = new Vector3(PlayerT.position.x + 2.9f, PlayerT.position.y - 1.8f);
        HandRegularPositionV2 = HandRegularPosition;
        transform.position = HandRegularPosition;
        if (IsCalm == true)
        {
            destinationStart = 0f;
            destinationEnd = 0f;
        }
    }
//-------------------------------------------------------------------------------------- 



//--------------------------------------------------------------------------------------
    private void Attack()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            IsCalm = false;
        }
    }
//--------------------------------------------------------------------------------------




//--------------------------------------------------------------------------------------
    private void MouseLookDirection()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseLookDirection1 = target - PlayerT.position;
        mouseRayCast = new Ray(PlayerT.position, MouseLookDirection1);
        HandAttackPosition = mouseRayCast.GetPoint(7);
    } 
//--------------------------------------------------------------------------------------



//--------------------------------------------------------------------------------------
    private void LightAttack()
    {
        if ( (IsCalm == false) && (OperateHand == 0) )
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
                    IsCalm = true;
                    LightAttacked = false;
                    HandBack = false;
                    OperateHand = 1;
                    destinationStart = 0f;
                    destinationEnd = 0f;
                }
            }
        }
    }
//--------------------------------------------------------------------------------------




















}
