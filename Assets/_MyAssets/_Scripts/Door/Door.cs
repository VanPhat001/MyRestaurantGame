using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Collider _collider;

    private readonly int OpenFrontState = Animator.StringToHash("openFront");
    private readonly int OpenBackState = Animator.StringToHash("openBack");

    public void OpenFront(bool value)
    {
        _animator.SetBool(OpenFrontState, value);
    }

    public void OpenBack(bool value)
    {
        _animator.SetBool(OpenBackState, value);
    }

    void Start()
    {
        _collider.isTrigger = false;
    }


    void OnCollisionEnter(Collision other)
    {
        if (1 << other.gameObject.layer != _playerLayer.value)
        {
            return;
        }

        var dir = this.transform.position - other.transform.position;
        if (dir.z > 0)
        {
            OpenFront(true);
            OpenBack(false);
        }
        else
        {
            OpenFront(false);
            OpenBack(true);
        }

        _collider.isTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (1 << other.gameObject.layer != _playerLayer.value)
        {
            return;
        }

        OpenFront(false);
        OpenBack(false);
        _collider.isTrigger = false;
    }

}