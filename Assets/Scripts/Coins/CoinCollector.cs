using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public static CoinCollector Instance;

    private float _coins;

    public float Coins
    {
        get
        {
            return _coins;
        }
        set
        {
            _coins = value;
        }
    }

    private void Awake() 
    {
        Instance = this;
    }

    
}
