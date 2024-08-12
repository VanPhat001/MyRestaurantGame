using System.Collections;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;


    void OnCollisionEnter(Collision other)
    {
        if (1 << other.gameObject.layer != _playerLayer.value)
        {
            return;
        }

        var itemManger = PlayerManager.Singleton.ItemManager;
        var number = itemManger.NumberItem(ItemName.Grabage);
        itemManger.RemoveItem(ItemName.Grabage, number);
    }
}