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

        if (!_seat.HasGrabage())
        {
            return;
        }

        _seat.ReleaseGarbage();
        var grabage = Instantiate(_seat.Grabage);
        grabage.name = ItemName.Grabage.ToString();
        grabage.SetActive(true);
        PlayerManager.Singleton.ItemManager.CollectItem(ItemName.Grabage, new() { grabage });
    }
}