using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class FoodCollectorMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private SandwichOven _sandwichOven;
    [SerializeField] private Transform _sandwich;
    private FoodCollectorManager _manager;

    void Start()
    {
        _manager = GetComponentInParent<FoodCollectorManager>();
        ResetAction();
    }

    void ResetAction()
    {
        _sandwich.gameObject.SetActive(false);
        _agent.isStopped = true;
        _agent.SetDestination(_sandwichOven.SpawnPoint.position);
    }


    void Update()
    {
        if (_sandwich.gameObject.activeSelf)
        {
            MoveToCounter();
        }
        else
        {
            MoveToSandwichOven();
        }
    }

    void MoveToCounter()
    {
        if (!ReachTarget())
        {
            return;
        }

        // --- OnPutSandwich ---
        ServiceCounter.Singleton.AddItem(ItemName.Sandwich, 1);
        ResetAction();
    }

    void MoveToSandwichOven()
    {
        if (!_sandwichOven.HasSandwich())
        {
            _manager.Anim.SetWalk(false);
            return;
        }

        if (!ReachTarget())
        {
            _agent.isStopped = false;
            _manager.Anim.SetWalk(true);
            return;
        }

        // --- OnPickSandwich ---
        var go = _sandwichOven.PopSandwich(1)[0];
        go.GetComponent<IReleaseable>().Release();
        _sandwich.gameObject.SetActive(true);
        _agent.SetDestination(ServiceCounter.Singleton.Checkpoint.position);
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