using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 100f;

    private Vector3 direction;

    private Transform _transfrom;


    // Start is called before the first frame update
    void Start()
    {
        _transfrom = transform;
    }

    public void SetDirection(Vector3 internalDirection)
    {
        this.direction = internalDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var step = speed * Time.fixedDeltaTime;
        _transfrom.position += direction * step;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Debug.Log("Necro Hit: " + other.gameObject);
            PlayerController.staticController.GetComponent<DamageablePlayer>().UpdateHealth(-35);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Item")
        {
            other.GetComponent<Destructible>().Damage();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        else if (other.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }
    }
}
