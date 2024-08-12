using UnityEngine;

public class CustomerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int WalkState = Animator.StringToHash("walk");
    private readonly int RunState = Animator.StringToHash("run");
    private readonly int EatState = Animator.StringToHash("eat");
    private readonly int EatEndState = Animator.StringToHash("eatEnd");


    public void SetWalk(bool value)
    {
        _animator.SetBool(WalkState, value);
    }

    public void SetRun(bool value)
    {
        _animator.SetBool(RunState, value);
    }

    public void SetEatEnd(bool value)
    {
        _animator.SetBool(EatEndState, value);
    }

    public void SetEatTrigger(bool activeTrigger)
    {
        if (activeTrigger)
        {
            _animator.SetTrigger(EatState);
        }
        else
        {
            _animator.ResetTrigger(EatState);
        }
    }

}