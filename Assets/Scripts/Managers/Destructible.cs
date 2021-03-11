using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private int health = 3;
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    // SelfDamage
    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            gameObject.SetActive(false); // Destroy(this.gameObject);
            TriggerDrop();
            return;
        }
        else
        {
            ChangeState(health);
        }
    }

    public void ChangeState(int state)
    {
        spriteRenderer.sprite = sprites[state - 1]; // state - 1 because indexation of array begins from 0
    }

    public void TriggerDrop()
    {
        GetComponent<CoinDrop>().InstantiateRandomAmoutOfCoins();
        if (gameObject.CompareTag("Item"))
        {
            GetComponent<HealPotionDrop>().RandomlyInstantiateHealPotion();
        }
    }
}
