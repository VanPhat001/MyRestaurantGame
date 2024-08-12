using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GarbageCollectorMoverment : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _trash;
    [SerializeField] private GameObject _grabage;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private GarbageCollectorManager _manager;
    private float _timer = .5f;
    private Seat _seat = null;
    private bool _isMoveToTrash = false;


    void Start()
    {
        _manager = this.GetComponentInParent<GarbageCollectorManager>();
        _virtualCamera.Priority = 9;
        Reset();
    }


    void Update()
    {
        if (_isMoveToTrash)
        {
            MoveToTrash();
        }
        else if (_seat != null)
        {
            MoveToSeat();
        }
        else
        {
            _timer -= Time.deltaTime;
            if (_timer > 0)
            {
                return;
            }

            _timer = .5f;
            FindGarbage();
        }
    }

    void FindGarbage()
    {
        var list = SeatManager.Singleton.FindGarbage();
        if (list.Count == 0)
        {
            return;
        }

        var minDistance = float.MaxValue;
        Seat minSeat = null;
        list.ForEach(item =>
        {
            var d = Vector3.Distance(item.SeatPoint.position, this.transform.position);
            if (minDistance > d)
            {
                minSeat = item;
                minDistance = d;
            }
        });

        if (minSeat == null)
        {
            return;
        }
        // --- OnMoveToSeat ---
        _seat = minSeat;
        _manager.Anim.SetWalk(true);
        _agent.isStopped = false;
        StartVirtualCamera();
    }

    void MoveToSeat()
    {
        _agent.SetDestination(_seat.SeatPoint.position);

        if (ReachTarget())
        {
            // --- OnMoveToTrash ---
            _isMoveToTrash = true;
            _seat.ReleaseGarbage();
            _grabage.SetActive(true);
        }
    }

    void MoveToTrash()
    {
        _agent.SetDestination(_trash.position);

        if (ReachTarget())
        {
            // --- OnThrowGrabage ---
            ThrowGrabage();
        }
    }

    void ThrowGrabage()
    {
        Reset();
        StopVirutalCamera();
    }

    void Reset()
    {
        _grabage.SetActive(false);
        _timer = .5f;
        _agent.isStopped = true;
        _isMoveToTrash = false;
        _manager.Anim.SetWalk(false);
        _seat = null;
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


    void StartVirtualCamera()
    {
        _virtualCamera.Priority = 11;
    }

    void StopVirutalCamera()
    {
        _virtualCamera.gameObject.SetActive(false);
    }
}