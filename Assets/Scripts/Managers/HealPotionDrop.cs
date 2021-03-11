using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotionDrop : MonoBehaviour
{
    int randomValue;
    public void RandomlyInstantiateHealPotion()
    {
        randomValue = Random.Range(0, 100);
        if (randomValue < 15)
        {
            Instantiate(Resources.Load("HealPotion"),
            transform.position + new Vector3(Random.Range(0, 2), Random.Range(0, 2), 0),
            Quaternion.identity
            );
        }
    }
}
