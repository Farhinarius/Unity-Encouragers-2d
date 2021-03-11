using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DamageablePlayer : MonoBehaviour
{
    public float maxHealth;
    private float health;
    public float healthRegenValue;
    private HealthBar healthBar;

    // iframe vairalbes
    public float timeInvincible = 1f;
    public float invincibleTimer;

    void Start()
    {
        health = maxHealth;
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
    }

    public void Damage(int damageAmount)
    {
        if (invincibleTimer < 0)
        {
            health += damageAmount;
            if (health > 0)
            {
                healthBar.SetValue(health / maxHealth);

                if (gameObject.GetComponent<Destructible>() != null)
                    GetComponent<Destructible>().ChangeState((int)health);

                invincibleTimer = timeInvincible;
                // Debug.Log("Health has been reduced: " + health / maxHealth);
            }
            else 
            {
                GameManager.instance.Defeat();
            }

        }
    }

    public void Heal(int healAmount)
    {
        health = Mathf.Clamp(health + healAmount, 0, maxHealth);
        healthBar.SetValue(health / maxHealth);

        if (gameObject.GetComponent<Destructible>() != null)
            GetComponent<Destructible>().ChangeState((int)health);
    }

    public void UpdateHealth(int amount)
    {
        if (amount < 0)
        {
            Damage(amount);
        }
        else 
        {
            Heal(amount);
        }
    }

    private void Update() 
    {
        if (invincibleTimer >= 0) invincibleTimer -= Time.deltaTime;

/*         if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateHealth(10);
        } */
    }

    private void FixedUpdate() {
        // slow health recovery
        if (health < maxHealth)
        {
            health += healthRegenValue;
            healthBar.SetValue(health / maxHealth);
        }
    }
}
