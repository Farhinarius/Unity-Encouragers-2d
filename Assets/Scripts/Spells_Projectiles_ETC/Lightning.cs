using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    Quaternion lightningDirection;
    
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start() 
    {
        AssignRandomSprite();
        StartCoroutine("WaitForDestroy");
    }

    private void AssignRandomSprite()
    {
        spriteRenderer.sprite =  sprites[ Random.Range(0,3) ];
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<DamageableEnemy>().Damage(2);
        }
        else if (other.tag == "Item")
        {
            other.GetComponent<Destructible>().Damage();
        }
    }
}

