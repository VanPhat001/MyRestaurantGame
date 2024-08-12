using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;


    void Start()
    {
        objects.ForEach(item => item.SetActive(false));
    }
}