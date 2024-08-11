using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    private PlayerManager _manager;
    private float _coin = 0;

    void Start()
    {
        _manager = this.GetComponentInParent<PlayerManager>();
    }

    public float GetCoin()
    {
        return _coin;
    }

    public void AddCoin(float value)
    {
        _coin += value;
        GameSceneUIManager.Singleton.SetCoinText(_coin);
    }

    public void RemoveCoin(float value)
    {
        AddCoin(-value);
    }

}