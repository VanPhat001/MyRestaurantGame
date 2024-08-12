using Cinemachine;
using UnityEngine;


[RequireComponent(typeof(CinemachineVirtualCamera))]
public class MoveToView : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float _time = 3f;
    private float _timer = 0;

    void Start()
    {
        _virtualCamera.Priority = 11;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _time)
        {
            _virtualCamera.Priority = 0;
            Destroy(this.gameObject, _time);
        }
    }
}