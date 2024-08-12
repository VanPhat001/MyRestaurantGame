using System.Collections.Generic;
using UnityEngine;

public class CoinArea : MonoBehaviour
{
    [SerializeField] private bool _autoFill = true;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Transform _container;
    [SerializeField] private float _totalCoin = 100;
    [SerializeField] private float _coinHeight = .2f;
    [SerializeField] private int _coinNumber = 20;


    void Start()
    {
        if (_autoFill)
        {
            AutoFillCoin(_totalCoin, _coinNumber);
        }
    }

    void AutoFillCoin(float total, int coinNumber)
    {
        float value = total / coinNumber;
        for (int i = 0; i < coinNumber; i++)
        {
            AddCoin(value);
            // var coin = CoinPool.Singleton.Get(CoinPool.CoinName.CoinStar, this.transform).GetComponent<Coin>();
            // coin.transform.position = _spawnPoints[i % _spawnPoints.Count].position + _coinHeight * (int)(i / _spawnPoints.Count) * Vector3.up;
            // coin.Init(true);
            // coin.SetValue(value);
        }
    }

    public void AddCoin(float value)
    {
        var coin = CoinPool.Singleton.Get(CoinPool.CoinName.CoinStar, _container).GetComponent<Coin>();
        var currentNumberCoin = _container.childCount;
        coin.transform.position = _spawnPoints[currentNumberCoin % _spawnPoints.Count].position + _coinHeight * (int)(currentNumberCoin / _spawnPoints.Count) * Vector3.up;
        coin.Init(true);
        coin.SetValue(value);
    }
}