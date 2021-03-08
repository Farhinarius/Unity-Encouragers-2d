using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AProjectile : MonoBehaviour
{
    public float speed = 100f;
    private Vector3 direction;
    private Transform _transfrom;
    public Action projectileBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        _transfrom = transform;
        projectileBehaviour = null;
    }

    public void SetDirection(Vector3 internalDirection)
    {
        this.direction = internalDirection;
        projectileBehaviour = spellStepBehaviour;
    }

    public void SetSpeed(float speedValue)
    {
        this.speed = speedValue;
    }

    void FixedUpdate()
    {
        if (projectileBehaviour != null)
            projectileBehaviour();
    }

    public void spellStepBehaviour()
    {
        var step = speed * Time.fixedDeltaTime;
        _transfrom.position += direction * step;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<DamageableEnemy>().Damage(1);
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
    }

}
