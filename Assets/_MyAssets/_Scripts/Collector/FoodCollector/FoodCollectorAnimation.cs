using UnityEngine;

public class FoodCollectorAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int WalkState = Animator.StringToHash("walk");


    public void SetWalk(bool value)
    {
        _animator.SetBool(WalkState, value);
    }
}