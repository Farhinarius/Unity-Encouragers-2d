using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance = null;

    private int coins;

    public int Coins { get => coins; private set => coins = value; }

    private void Awake()
    {
        Debug.Log("CoinManager Awake");
        if (instance == null) instance = this;
    }
    
    private void Start() {
        
    }

    public void AddCoin(int pickedCoins)
    {
        this.Coins += pickedCoins; 
        GameManager.instance.UpdateUICoinNumber(Coins);
    }


}
