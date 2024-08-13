using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField] private Transform _itemStartPoint;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _collectCoinSound;
    [SerializeField] private AudioClip _grabageSound;
    private PlayerManager _manager;
    private float _coin = 0;
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
        PlayCollectCoinSound();
    }

    void PlayCollectCoinSound()
    {
        _audioSource.PlayOneShot(_collectCoinSound);
    }

    void PlayGrabageSound()
    {
        _audioSource.PlayOneShot(_grabageSound);
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
            go.localPosition = _itemStartPoint.childCount * Vector3.up * _itemOffsetHeight;
        }

        _collectedItem[itemName] += number;

        if (itemName == ItemName.Grabage)
        {
            PlayGrabageSound();
        }
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

    public int NumberItem(ItemName itemName)
    {
        if (_collectedItem.TryGetValue(itemName, out var number))
        {
            return number;
        }
        return 0;
    }

    public void RemoveItem(ItemName itemName, int count)
    {
        if (!_collectedItem.ContainsKey(itemName))
        {
            _collectedItem[itemName] = 0;
        }

        if (_collectedItem[itemName] < count)
        {
            Debug.LogError("_collectedItem value is less than count");
        }

        _collectedItem[itemName] -= count;
        int d = 0;
        for (int i = _itemStartPoint.childCount - 1; i >= 0; i--)
        {
            var item = _itemStartPoint.GetChild(i);
            if (item.name != itemName.ToString())
            {
                continue;
            }
            item.GetComponent<IReleaseable>().Release();
            d++;

            if (d >= count)
            {
                return;
            }
        }

        if (itemName == ItemName.Grabage)
        {
            PlayGrabageSound();
        }

        StartCoroutine(UpdateUIAfterNextFrame());
    }

    IEnumerator UpdateUIAfterNextFrame()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < _itemStartPoint.childCount; i++)
        {
            _itemStartPoint.GetChild(i).localPosition = Vector3.up * _itemOffsetHeight * i;
        }
    }

}