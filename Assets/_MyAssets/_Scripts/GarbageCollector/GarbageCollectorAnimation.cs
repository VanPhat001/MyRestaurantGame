using System.Security.Cryptography;
using UnityEngine;

public class GarbageCollectorAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Animator Anim => _animator;

    private readonly int WalkState = Animator.StringToHash("walk");


    public void SetWalk(bool value)
    {
        _animator.SetBool(WalkState, value);
    }
}