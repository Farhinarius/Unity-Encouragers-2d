using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DamageablePlayer : MonoBehaviour
{
    public float maxHealth;
    private int health;
    public float healthRegenValue;
    public HealthBar healthBar;

    void Start()
    {
        health = (int) maxHealth;
        transform.GetChild(0).gameObject.SetActive(true);   // activate health canvas in gameplay
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            healthBar.SetHealthBarValue(health / maxHealth); // set hp on UI
            if (gameObject.GetComponent<Destructible>() != null) // if destructible
                GetComponent<Destructible>().ChangeState(health);   // state is health
        } 
        Debug.Log("Health: " + health / maxHealth);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(5);
        }
    }

    private void FixedUpdate() {
        if (healthBar.GetHealthBarValue() != maxHealth)
            healthBar.SetHealthBarValue(healthBar.GetHealthBarValue() + healthRegenValue / maxHealth);
    }
}
