using UnityEngine;

public class Marketing : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _decreaseSpeedRate = .3f;

    void OnEnable()
    {
        Customers.Singleton.SpawnTime *= 1 - _decreaseSpeedRate;
        Destroy(this.gameObject, 2);
    }
}