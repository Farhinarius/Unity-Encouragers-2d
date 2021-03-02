using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnemy : MonoBehaviour
{
    public int maxHealth;
    private int health;
    private HealthBar healthBar;

    void Start()
    {
        health = maxHealth;
        transform.GetChild(0).gameObject.SetActive(true);   // activate health canvas in gameplay
        healthBar = transform.GetComponentInChildren<HealthBar>(); // get health bar image
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            gameObject.SetActive(false); //or Destroy(gameObject);
            return;
        }
        else
        {
            Debug.Log("non zero health");
            healthBar.SetHealthBarValue(healthBar.GetHealthBarValue() - (float) damageAmount / maxHealth);
            if (gameObject.GetComponent<Destructible>() != null) // if destructible
                GetComponent<Destructible>().ChangeState(health);   // state is health
        }
        Debug.Log(health);
    }
}
