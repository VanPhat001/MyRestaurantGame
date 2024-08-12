using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private CustomerMoverment _customerMoverment;
    public CustomerMoverment CustomerMoverment => _customerMoverment;

    [SerializeField] private CustomerAnimation _customerAnimation;
    public CustomerAnimation Anim => _customerAnimation;

    [SerializeField] private Transform _viewPoint;
    public Transform ViewPoint => _viewPoint;


    void Start()
    {

    }

    public void Init(bool useViewPoint, Transform pos, int waitPointIndex, Transform exitPoint)
    {
        ViewPoint.gameObject.SetActive(useViewPoint);
        CustomerMoverment.SetCustomerWaitPointIndex(waitPointIndex);
        CustomerMoverment.SetTarget(pos);
        CustomerMoverment.SetExitPoint(exitPoint);
    }

    public void MoveToNextWaitPoint(List<Transform> posList)
    {
        var nextPointIndex = CustomerMoverment.GetCustomerWaitPointIndex() - 1;
        CustomerMoverment.SetCustomerWaitPointIndex(nextPointIndex);
        CustomerMoverment.SetTarget(posList[nextPointIndex]);
    }

    public void ReceiveSandwichItem()
    {
        _customerMoverment.NextPeriod();
    }
}