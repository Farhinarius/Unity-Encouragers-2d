using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitForDestroy");
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }

/*     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Projectile")
        {
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = false;
        }
    } */
}
