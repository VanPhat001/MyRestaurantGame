using UnityEngine;

public class CoinPool : BasePool<CoinPool.CoinName>
{
    public enum CoinName
    {
        Coin1,
        CoinStar
    }


    public static CoinPool Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
    }

    protected override bool Compare(CoinName a, CoinName b)
    {
        return a == b;
    }

    protected override bool Compare(CoinName a, string b)
    {
        return a.ToString() == b;
    }
}