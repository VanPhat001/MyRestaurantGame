using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public static Customers Singleton { get; private set; }

    [SerializeField] private List<Transform> _waitPoints;
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _exitPoint;
    private bool _isFirstSpawn = true;
    private float _spawnTime = 13f;
    public float SpawnTime { get => _spawnTime; set => _spawnTime = value; }
    private float _firstSpawnDelay = 1f;
    private float _timer;
    private Queue<CustomerManager> _customerQueue = new();



    void Awake()
    {
        Singleton = this;
    }

    void Start()
    {
        _timer = _firstSpawnDelay;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0)
        {
            return;
        }

        if (IsEmptySeat())
        {
            return;
        }

        SpawnCustomer(_isFirstSpawn);
    }

    void SpawnCustomer(bool useViewPoint)
    {
        _isFirstSpawn = false;
        _timer = _spawnTime;

        var go = Instantiate(_customerPrefab, _startPoint.position, Quaternion.identity);
        var customer = go.GetComponent<CustomerManager>();
        customer.Init(useViewPoint, _waitPoints[CountUsedPoint()], CountUsedPoint(), _exitPoint);
        _customerQueue.Enqueue(customer);
    }

    int CountUsedPoint()
    {
        return _customerQueue.Count;
    }


    bool IsEmptySeat()
    {
        return CountUsedPoint() >= _waitPoints.Count;
    }

    public void Dequeue()
    {
        _customerQueue.Dequeue();
        _customerQueue.ToList().ForEach(item => item.MoveToNextWaitPoint(_waitPoints));
    }
}