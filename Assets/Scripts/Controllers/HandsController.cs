using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    private PlayerController player;
    private Vector3 handAttackPosition;
    static public bool rightIsCalm;
    static public bool leftIsCalm;
    public Transform leftHand;
    public Transform rightHand;
    private Vector3 leftHandRegular;
    private Vector3 rightHandRegular;
    private float destinationStart;
    private float destinationEnd;
    private int operateHand;
    private bool handBack;
    private bool lightAttacked;
    public Ray mouseRayCast;
    // Start is called before the first frame update
    void Start()
    {
        operateHand = 1;
        player = gameObject.GetComponent<PlayerController>();
        leftIsCalm = true;
        rightIsCalm = true;
        destinationStart = 0f;
        destinationEnd = 0f;
        handBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandsTransform();
        MouseLookDirection();
        Attack();
        LightAttack();
    }





    private void HandsTransform()
    {
            leftHandRegular = new Vector3(transform.position.x - 2.9f, transform.position.y - 1.8f);
            rightHandRegular = new Vector3(transform.position.x + 2.9f, transform.position.y - 1.8f);

            if (rightIsCalm == true)
            {
                rightHand.position = rightHandRegular;
            }
            if (leftIsCalm == true)
            {
                leftHand.position = leftHandRegular;
            }
            
    }
    private void Attack()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            if (operateHand == 1)
            {
                rightIsCalm = false;
            }
            if (operateHand == 2)
            {
                leftIsCalm = false;
            }
        }
    }


    private void MouseLookDirection()
    {

        mouseRayCast = new Ray(transform.position, player.LookDirection);
        handAttackPosition = mouseRayCast.GetPoint(7);
    } 

    private void LightAttack()
    {
        if (rightIsCalm == false)
        {
            if ( (rightHand.position != handAttackPosition) && (lightAttacked == false) )
            {   
                rightHand.position = Vector2.MoveTowards(rightHandRegular, handAttackPosition, destinationStart);
                destinationStart += Time.deltaTime * 75;
            }
            if (rightHand.position == handAttackPosition)
            {
                handBack = true;
            }

            if (handBack == true)        
            {
                lightAttacked = true;
                rightHand.position = Vector2.MoveTowards(handAttackPosition, rightHandRegular, destinationEnd);
                destinationEnd += Time.deltaTime * 75;
                
                
                if (rightHand.position == rightHandRegular)
                {
                    rightIsCalm = true;
                    lightAttacked = false;
                    handBack = false;
                    operateHand = 2;
                    destinationStart = 0f;
                    destinationEnd = 0f;
                }
            }
        }

        if (leftIsCalm == false)
        {
            if ( (leftHand.position != handAttackPosition) && (lightAttacked == false) )
            {   
                leftHand.position = Vector2.MoveTowards(leftHandRegular, handAttackPosition, destinationStart);
                destinationStart += Time.deltaTime * 75;
            }
            if (leftHand.position == handAttackPosition)
            {
                handBack = true;
            }

            if (handBack == true)        
            {   
                lightAttacked = true;
                leftHand.position = Vector2.MoveTowards(handAttackPosition, leftHandRegular, destinationEnd);
                destinationEnd += Time.deltaTime * 75;
                
                
                if (leftHand.position == leftHandRegular)
                {
                    leftIsCalm = true;
                    lightAttacked = false;
                    handBack = false;
                    operateHand = 1;
                    destinationStart = 0f;
                    destinationEnd = 0f;
                }
            }
        }
    }





}






