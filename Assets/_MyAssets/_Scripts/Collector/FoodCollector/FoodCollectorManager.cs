using UnityEngine;

public class FoodCollectorManager : MonoBehaviour
{
    [SerializeField] FoodCollectorMovement _moverment;
    public FoodCollectorMovement Movement => _moverment;

    [SerializeField] FoodCollectorAnimation _animation;
    public FoodCollectorAnimation Anim => _animation;
}