using System;
using UnityEngine;

public class ServiceCounter : MonoBehaviour
{
    public static ServiceCounter Singleton { get; private set;}

    [SerializeField] private Transform _container;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _checkpoint;
    public Transform Checkpoint => _checkpoint;
    [SerializeField] private CoinArea _coinArea;
    private float _itemOffsetHeight = .1f;


    void Awake()
    {
        Singleton = this;
    }

    public void AddItem(ItemName item, int number)
    {
        FoodPool.FoodName foodName;
        switch (item)
        {
            case ItemName.Sandwich:
                foodName = FoodPool.FoodName.Sandwich;
                break;

            default:
                throw new Exception();
        }

        for (int i = 0; i < number; i++)
        {
            var food = FoodPool.Singleton.Get(foodName);
            food.transform.position = _spawnPoint.position + Vector3.up * _itemOffsetHeight * _container.childCount;
            food.transform.SetParent(_container);
        }
    }

    public void RemoveItem(ItemName itemName, int number)
    {
        for (int i = _container.childCount - 1; i >= 0; i--)
        {
            var item = _container.GetChild(i);
            if (item.name != itemName.ToString())
            {
                continue;
            }

            item.GetComponent<IReleaseable>().Release();
            AddCoinToCoinArea();
            number--;
            if (number <= 0)
            {
                break;
            }
        }

        UpdateContainerChildren();
    }

    private void AddCoinToCoinArea()
    {
        _coinArea.AddCoin(3);
    }

    void UpdateContainerChildren()
    {
        for (int i = 0; i < _container.childCount; i++)
        {
            var item = _container.GetChild(i);
            item.transform.position = _spawnPoint.position + Vector3.up * _itemOffsetHeight * i;
        }
    }

    public bool IsHaveItem(ItemName itemName, int number)
    {
        int d = 0;
        foreach (Transform item in _container)
        {
            if (item.name != itemName.ToString())
            {
                continue;
            }

            d++;
            if (d >= number)
            {
                return true;
            }
        }
        return false;
    }
}