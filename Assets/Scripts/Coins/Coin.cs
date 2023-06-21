using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _value;

    public void AddCoin()
    {
        CoinCollector.Instance.Coins += _value;

        Debug.Log(CoinCollector.Instance.Coins);
    }
}
