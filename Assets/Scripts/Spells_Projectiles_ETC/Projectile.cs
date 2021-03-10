using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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

    public void SetSpeed(float speedValue)
    {
        this.speed = speedValue;
    }

    void FixedUpdate()
    {
        var step = speed * Time.fixedDeltaTime;
        _transfrom.position += direction * step;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy")
        {
            other.GetComponent<DamageableEnemy>().Damage(PlayerController.staticController.damage);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Item")
        {
            if (other.GetComponent<Destructible>() != null)
                other.GetComponent<Destructible>().Damage();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
