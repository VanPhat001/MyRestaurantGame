using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] private float _time = 1f;

    void Update()
    {
        _time -= Time.deltaTime;

        if (_time <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}