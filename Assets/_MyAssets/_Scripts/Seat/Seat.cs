using UnityEngine;

public class Seat : MonoBehaviour
{
    [SerializeField] private Transform _seatPoint;
    public Transform SeatPoint => _seatPoint;
    [SerializeField] private GameObject _grabage;
    public GameObject Grabage => _grabage;

    [SerializeField] private Transform _foodPoint;
    public Transform FoodPoint => _foodPoint;


    private bool _isUsed = false;
    public bool IsUsed => _isUsed;

    public void SetIsUsed(bool value)
    {
        _isUsed = value;
    }

    public void PutSandwich()
    {
        var food = FoodPool.Singleton.Get(FoodPool.FoodName.Sandwich, _foodPoint).transform;
        food.position = _foodPoint.position;
        food.rotation = _foodPoint.rotation;
    }

    public void RelaseSandwich()
    {
        _foodPoint.GetChild(0).GetComponent<IReleaseable>().Release();
    }

    public void PutGarbage()
    {
        _grabage.SetActive(true);
    }

    public void ReleaseGarbage()
    {
        if (!HasGrabage())
        {
            return;
        }
        
        _grabage.SetActive(false);
        SetIsUsed(false);
    }

    public bool HasGrabage()
    {
        return _grabage.activeSelf;
    }
}