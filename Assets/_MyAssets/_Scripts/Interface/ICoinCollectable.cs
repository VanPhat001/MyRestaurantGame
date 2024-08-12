using UnityEngine;

public interface ICoinCollectable
{
    public float GetCurrentCoin();
    public void DescreaseCoin(float value);
    public Transform GetCoinStartPoint();
}