using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _cam;

    void Start()
    {
        _cam = Camera.main.transform;
    }

    void Update()
    {
        this.transform.LookAt(_cam.position);
    }
}