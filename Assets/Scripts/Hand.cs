using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Need to fix DoubleDamageCollider!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if ( (HandsController.rightIsCalm == false) || (HandsController.leftIsCalm == false) )
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<DamageableEnemy>().Damage(1);
                other.GetComponent<Rigidbody2D>().AddForce(player.LookDirection * 1000, ForceMode2D.Impulse);

            }
            else if (other.tag == "Item")
            {
                other.GetComponent<Destructible>().Damage();
                other.GetComponent<Rigidbody2D>().AddForce(player.LookDirection*1000, ForceMode2D.Impulse);


            }
        }
    }



}
