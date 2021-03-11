using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    public int pickedCoinsValue = 10;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GetComponent<CoinManager>().AddCoin(+pickedCoinsValue);
            gameObject.SetActive(false);
        }
    }
}
