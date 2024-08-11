using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolItem<T>
{
    public T Name;
    public GameObject Prefab;
}


public abstract class BasePool<T> : MonoBehaviour
{
    [SerializeField] private List<PoolItem<T>> _poolItemList;
    private List<GameObject> _pool = new();

    public GameObject Get(T itemName, Transform parent = null, Action<GameObject> callback = null)
    {
        return Get(itemName.ToString(), parent, callback);
    }

    public GameObject Get(string itemName, Transform parent = null, Action<GameObject> callback = null)
    {
        var go = FindInPool(itemName);

        if (go != null)
        {
            _pool.Remove(go);
        }
        else
        {
            go = CreateNewGO(itemName);
            go.name = itemName;
        }

        callback?.Invoke(go);

        go.transform.SetParent(parent);
        go.SetActive(true);
        return go;
    }


    public void Release(GameObject go)
    {
        _pool.Add(go);
        go.transform.SetParent(this.transform);
        go.SetActive(false);
    }


    private GameObject CreateNewGO(string itemName)
    {
        var poolItem = FindPoolItem(itemName);
        if (poolItem == null)
        {
            Debug.LogError($"ItemName [{itemName}] not exists in pool");
            return null;
        }

        var go = Instantiate(poolItem.Prefab);
        go.SetActive(false);
        return go;
    }


    protected GameObject FindInPool(string itemName)
    {
        return _pool.Find(item => item.name == itemName);
    }


    protected PoolItem<T> FindPoolItem(string itemName)
    {
        return _poolItemList.Find(item => Compare(item.Name, itemName));
    }

    protected PoolItem<T> FindPoolItem(T itemName)
    {
        return _poolItemList.Find(item =>  Compare(item.Name, itemName));
    }

    protected abstract bool Compare(T a, T b);
    protected abstract bool Compare(T a, string b);
}