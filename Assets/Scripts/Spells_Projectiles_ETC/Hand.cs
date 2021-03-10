using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public PlayerController player;
    private BoxCollider2D handCollider;
    private int damage;
    private float iFrameTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        handCollider = GetComponent<BoxCollider2D>();
        iFrameTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (handCollider.enabled == false)
        {
            if (iFrameTime >= 0.01f)
            {
                handCollider.enabled = true;
            }
            else if (iFrameTime < 0.01f)
            {
                iFrameTime += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if ( (HandsController.rightIsCalm == false) || (HandsController.leftIsCalm == false) )
        {
            if (other.tag == "Enemy")
            {

                other.GetComponent<DamageableEnemy>().Damage(PlayerController.staticController.damage);
                handCollider.enabled = false;
                iFrameTime = 0f;
                other.GetComponent<Rigidbody2D>().AddForce(player.LookDirection * 3000, ForceMode2D.Impulse);

            }
            else if (other.tag == "Item")
            {
                other.GetComponent<Destructible>().Damage();
                handCollider.enabled = false;
                iFrameTime = 0f;
                other.GetComponent<Rigidbody2D>().AddForce(player.LookDirection*1000, ForceMode2D.Impulse);
            }
        }
    }



}
