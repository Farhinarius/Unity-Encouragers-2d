using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    int randomValue;
    public void InstantiateRandomAmoutOfCoins() 
    {
        if (gameObject.CompareTag("Enemy"))
        {
            randomValue = Random.Range(1, 4);
        }
        else
        {
            randomValue = Random.Range(0, 4);
        }
        if (randomValue > 1)
        {
            for (int i = 0; i < Random.Range(2,5); i++)
            {
                Instantiate(Resources.Load("Coin"),
                transform.position + new Vector3(Random.Range(0,4), Random.Range(0, 4), 0),
                Quaternion.identity
                );
            }
        }
    }
}
