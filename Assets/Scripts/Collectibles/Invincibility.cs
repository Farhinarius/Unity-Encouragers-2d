using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


        private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            other.GetComponent<BuffManager>().invincibilityIsActive = true;
            other.GetComponent<DamageablePlayer>().invincibleTimer = 999f;
            Destroy(gameObject);
        }
    }

}
