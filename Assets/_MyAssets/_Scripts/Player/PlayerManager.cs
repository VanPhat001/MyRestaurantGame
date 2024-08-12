using UnityEngine;

public class PlayerManager : MonoBehaviour, ICoinCollector, ICoinCollectable
{
    public static PlayerManager Singleton { get; private set; }


    [SerializeField] private Transform _model;
    public Transform Model  => _model;

    [SerializeField] private PlayerMoverment _moverment;
    public PlayerMoverment Moverment => _moverment;

    [SerializeField] private PlayerAnimation _animation;
    public PlayerAnimation Anim => _animation;

    [SerializeField] private PlayerItemManager _itemManager;
    [SerializeField] private Transform _coinDestinatoin;

    public PlayerItemManager ItemManager  => _itemManager;



    void Awake()
    {
        Singleton = this;
    }

    public void CollectCoin(float value)
    {
        _itemManager.AddCoin(value);        
    }

    public Transform GetCoinDestination()
    {
        return _coinDestinatoin;
    }

    public float GetCurrentCoin()
    {
        return _itemManager.GetCoin();
    }

    public void DescreaseCoin(float value)
    {
        _itemManager.DecreaseCoin(value);
    }

    public Transform GetCoinStartPoint()
    {
        return _coinDestinatoin;
    }
}