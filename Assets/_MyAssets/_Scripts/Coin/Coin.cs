using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float value = 1;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Collider _collider;


    void OnEnable()
    {
        _collider.enabled = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer != _playerLayer.value)
        {
            return;
        }

        var coinCollector = other.transform.root.GetComponent<ICoinCollector>();
        coinCollector.CollectCoin(value);
        _collider.enabled = false;

        PlayCollectionEffect(coinCollector.GetCoinDestination());
    }

    void PlayCollectionEffect(Transform destination)
    {
        this.transform.LeanMoveY(this.transform.position.y + 1f, .3f).setEaseInExpo().setOnComplete(() =>
        {
            this.transform.LeanMove(destination.position, .14f).setEaseOutExpo().setOnComplete(() =>
            {
                Release();
            });
        });
    }


    public void Release()
    {
        CoinPool.Singleton.Release(this.gameObject);
    }
}