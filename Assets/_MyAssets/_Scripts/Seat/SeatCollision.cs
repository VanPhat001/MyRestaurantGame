using UnityEngine;

public class SeatCollision : MonoBehaviour
{
    [SerializeField] private Seat _seat;
    [SerializeField] private LayerMask _playerLayer;

    void OnCollisionEnter(Collision other)
    {
        if (1 << other.gameObject.layer != _playerLayer.value)
        {
            return;
        }

        _seat.ReleaseGarbage();
    }
}