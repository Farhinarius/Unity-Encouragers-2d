using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    public int pickedUpValue = 10;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GetComponent<CoinManager>().AddCoin(+pickedUpValue);
            Destroy(this.gameObject);
        }
    }
}
