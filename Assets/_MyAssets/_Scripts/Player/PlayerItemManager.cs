using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField] private Transform _itemStartPoint;
    private PlayerManager _manager;
    private float _coin = 0;
    private int _itemCount = 0;
    private float _itemOffsetHeight = .1f;
    private Dictionary<ItemName, int> _collectedItem = new();
    private Dictionary<ItemName, int> _capacity = new() {
        { ItemName.Sandwich, 3 },
    };



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

    public void DecreaseCoin(float value)
    {
        AddCoin(-value);
    }


    public void CollectItem(ItemName itemName, List<GameObject> goList)
    {
        var number = goList.Count;
        if (!_collectedItem.ContainsKey(itemName))
        {
            _collectedItem.Add(itemName, 0);
        }

        for (int i = 0; i < number; i++)
        {
            var go = goList[i].transform;
            go.SetParent(_itemStartPoint);
            go.localPosition = (i + _itemCount + 1) * Vector3.up * _itemOffsetHeight;
        }

        _collectedItem[itemName] += number;
        _itemCount += number;
    }

    public int NumberItemGet(ItemName itemName)
    {
        if (!_capacity.ContainsKey(itemName))
        {
            return -1;
        }

        if (!_collectedItem.ContainsKey(itemName))
        {
            _collectedItem[itemName] = 0;
        }

        return _capacity[itemName] - _collectedItem[itemName];
    }

}