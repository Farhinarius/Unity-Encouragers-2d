using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnemy : MonoBehaviour
{
    public int maxHealth;
    private int health;
    private HealthBar healthBar;

    private Destructible destructibleComponent;

    void Start()
    {
        health = maxHealth;
        transform.GetChild(0).gameObject.SetActive(true);   // activate health canvas in gameplay
        healthBar = gameObject.GetComponentInChildren<HealthBar>(); // get health bar image
        
        if (gameObject.GetComponent<Destructible>() != null)
            destructibleComponent = GetComponent<Destructible>();
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            gameObject.SetActive(false); //or Destroy(gameObject);
            if (destructibleComponent != null)
                destructibleComponent.TriggerDrop();
            return;
        }
        else
        {
            healthBar.SetValue((float) health / maxHealth);
            if (destructibleComponent != null)
                destructibleComponent.ChangeState(health);   // state is health
        }
        // Debug.Log(health);
    }
}
