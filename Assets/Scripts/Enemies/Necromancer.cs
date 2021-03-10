using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// if player not near, idle
// if self within 10 meters from the player start Attacking
// if player left start finding
// if finded, start attacking again

public class Necromancer : MonoBehaviour
{
    public float speed;

    public float timeToTurn = 1.5f;
    private float turnTimer;

    public const float maxSpellCooldown = 0.7f;
    private float spellCooldown;
    Vector2 direction = Vector2.right;

    public delegate void EnemyBehaviour();
    public EnemyBehaviour stateBehaviour;

    Vector2 playerDirection;

    private void Start() 
    {
        TransitTo(Idle);
    }


    public void Idle()
    {
        turnTimer -= Time.deltaTime;
        transform.Translate(speed * direction * Time.deltaTime);

        if (turnTimer < 0)
        {
            turnTimer = timeToTurn;
            direction = -direction;
        }

        // transition condition
        if (Vector2.Distance(transform.position, PlayerController.staticController.transform.position) <= 80)
        {
            TransitTo(Attacking);
        }
    }

    public void FindingPlayer()
    {
        float angle = Random.Range(-0.5f, 0.5f) + Mathf.Atan2(PlayerController.staticController.transform.position.y - transform.position.y, PlayerController.staticController.transform.position.x - transform.position.x);
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;

        if (Vector2.Distance(transform.position, PlayerController.staticController.transform.position) <= 80)
        {
            TransitTo(Attacking);
        }
    }

    public void Attacking()
    {
        LaunchProjectile();

        if (Vector2.Distance(transform.position, PlayerController.staticController.transform.position) > 80)
        {
            TransitTo(FindingPlayer);
        }
    }

    private void LaunchProjectile()
    {
        if (spellCooldown <= 0)
        {
            GameObject projectile = Instantiate(Resources.Load("EnemySpell"), transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<EnemyProjectile>().SetDirection( (PlayerController.staticController.transform.position  - transform.position ).normalized );
            spellCooldown = maxSpellCooldown;
        }
    }

    void FixedUpdate()
    {
        if (spellCooldown > 0) spellCooldown -= Time.fixedDeltaTime;
        UpdateState();
    }

    public void TransitTo(EnemyBehaviour someBehaviour)
    {
        stateBehaviour = someBehaviour;
    }

    public void UpdateState()
    {
        if (stateBehaviour != null)
            stateBehaviour();
    }
}

/* public class FiniteStateMachine
{   
    public delegate void EnemyBehaviour();
    public EnemyBehaviour stateBehaviour;

    public void TransitTo(EnemyBehaviour someBehaviour)
    {
        stateBehaviour = someBehaviour;
    }

    public void Update() 
    {
        if (stateBehaviour != null)
            stateBehaviour();
    }
}
 */