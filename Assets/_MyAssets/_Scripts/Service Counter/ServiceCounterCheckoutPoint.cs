using UnityEngine;

public class ServiceCounterCheckoutPoint : MonoBehaviour
{
    [SerializeField] private ServiceCounter _serviceCounter;
    [SerializeField] private LayerMask _playerLayer;


    void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer != _playerLayer.value)
        {
            return;
        }

        var itemManager = PlayerManager.Singleton.ItemManager;
        var sandWichNumber = itemManager.NumberItem(ItemName.Sandwich);
        itemManager.RemoveItem(ItemName.Sandwich, sandWichNumber);
        _serviceCounter.AddItem(ItemName.Sandwich, sandWichNumber);
    }
}