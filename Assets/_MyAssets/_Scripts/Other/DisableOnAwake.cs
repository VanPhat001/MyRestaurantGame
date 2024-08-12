using System.Collections.Generic;
using UnityEngine;

public class DisableOnAwake : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _skipCount = 0;


    void Awake()
    {
        for (int i = _skipCount; i < _container.childCount; i++)
        {
            _container.GetChild(i).gameObject.SetActive(false);
        }
    }
}