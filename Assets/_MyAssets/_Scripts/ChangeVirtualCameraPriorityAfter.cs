using System.Collections;
using Cinemachine;
using UnityEngine;

public class ChangeVirtualCameraPriorityAfter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private int _priority;
    [SerializeField] private float _sec;

    void Start()
    {
        StartCoroutine(Excute());
    }

    IEnumerator Excute()
    {
        yield return new WaitForSeconds(_sec);
        _virtualCamera.Priority = _priority;
    }
}