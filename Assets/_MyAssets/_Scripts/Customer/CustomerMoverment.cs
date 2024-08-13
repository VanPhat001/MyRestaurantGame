using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMoverment : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _sandwichPoint;
    [SerializeField] private TMP_Text _statusText;
    private Transform _exitPoint;
    private CustomerManager _manager;
    private int _customerWaitPointIndex;
    private int _period = 0;
    public int Period;



    void Start()
    {
        _manager = this.GetComponentInParent<CustomerManager>();

        _agent.isStopped = false;
        _agent.speed = _moveSpeed;
        _statusText.gameObject.SetActive(false);
    }

    void Update()
    {
        switch (_period)
        {
            case 0:
                Period0();
                break;
            case 1:
                Period1();
                break;
            case 2:
                Period2();
                break;
            case 3:
                Period3();
                break;
            case 4:
                Period4();
                break;
            case 5:
                Period5();
                break;
        }
    }

    public void SetExitPoint(Transform exitPoint)
    {
        _exitPoint = exitPoint;
    }

    public void NextPeriod()
    {
        _period++;
        Period = _period;
    }

    // move to top queue
    void Period0()
    {
        if (ReachTarget())
        {
            _manager.Anim.SetWalk(false);
            if (_customerWaitPointIndex == 0)
            {
                // --- OnPeriod1 ---
                NextPeriod();
            }
            return;
        }

        _agent.destination = _target.position;
        _manager.Anim.SetWalk(true);
    }

    // waiting item
    private float _timer = 0;

    void Period1()
    {
        // waiting sandwich
        _timer -= Time.deltaTime;
        if (_timer > 0)
        {
            return;
        }

        _timer = 1f;
        if (!ServiceCounter.Singleton.IsHaveItem(ItemName.Sandwich, 1))
        {
            return;
        }

        // --- OnPeriod2 ---
        _manager.ReceiveSandwichItem();
        var sandwich = FoodPool.Singleton.Get(FoodPool.FoodName.Sandwich, _sandwichPoint).transform;
        sandwich.position = _sandwichPoint.position;
        ServiceCounter.Singleton.RemoveItem(ItemName.Sandwich, 1);
    }

    // waiting seat
    private Seat _seat;
    void Period2()
    {
        _seat = SeatManager.Singleton.FindEmptySeat();
        if (_seat == null)
        {
            _statusText.gameObject.SetActive(true);
            return;
        }

        // --- OnPeriod3 ---
        _statusText.gameObject.SetActive(false);
        _seat.SetIsUsed(true);
        NextPeriod();
        SetTarget(_seat.SeatPoint);
        Customers.Singleton.Dequeue();
        _manager.Anim.SetRun(true);
    }

    // move to table
    void Period3()
    {
        // Debug.Log("move to table");
        if (!ReachTarget())
        {
            return;
        }

        // --- OnPeriod4 ---
        _manager.Anim.SetRun(false);
        _manager.Anim.SetEatTrigger(true);
        _seat.PutSandwich();
        _sandwichPoint.GetChild(0).GetComponent<IReleaseable>().Release();
        NextPeriod();
    }

    // eating
    private float _eatTimer = 0;
    void Period4()
    {
        _eatTimer += Time.deltaTime;
        if (_eatTimer < 10)
        {
            return;
        }

        // --- OnPeriod5 ---
        _manager.Anim.SetEatEnd(true);
        _manager.Anim.SetWalk(true);
        SetTarget(_exitPoint);
        _seat.RelaseSandwich();
        _seat.PutGarbage();
        NextPeriod();
    }

    // quit
    void Period5()
    {
        if (!ReachTarget())
        {
            return;
        }

        Destroy(this.transform.root.gameObject);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _agent.destination = _target.position;
    }

    public void SetCustomerWaitPointIndex(int value)
    {
        _customerWaitPointIndex = value;
    }

    public int GetCustomerWaitPointIndex()
    {
        return _customerWaitPointIndex;
    }

    bool ReachTarget()
    {
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}