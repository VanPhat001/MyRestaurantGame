using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly int WalkState = Animator.StringToHash("walk");

    public void SetWalk(bool value)
    {
        _animator.SetBool(WalkState, value);
    }
}