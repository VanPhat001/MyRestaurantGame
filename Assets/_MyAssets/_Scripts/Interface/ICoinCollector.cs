using UnityEngine;

public interface ICoinCollector
{
    public void CollectCoin(float value);
    public Transform GetCoinDestination();
}