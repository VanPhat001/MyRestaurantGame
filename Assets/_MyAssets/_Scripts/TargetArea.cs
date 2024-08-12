using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetArea : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private float _coinNeeded = 100;
    [SerializeField] private float _tick = .05f;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] private List<GameObject> _unblockOnComplete;
    private bool _used = false;


    void Start()
    {
        UpdateUI(0);
    }


    private void UpdateUI(float percent)
    {
        _backgroundImage.fillAmount = percent;
        _coinText.text = (int)(_coinNeeded * (1 - percent)) + "";
    }

    private void Fill(ICoinCollectable coinCollectable)
    {
        if (_used)
        {
            return;
        }

        _used = true;
        var rate = .05f;
        var coin = _coinNeeded * rate;
        StartCoroutine(Test());
        IEnumerator Test()
        {
            float percent = 0;
            do
            {
                percent += rate;
                yield return new WaitForSeconds(_tick);
                coinCollectable.DescreaseCoin(coin);
                UpdateUI(percent);
                PlayCoinEffect(coinCollectable.GetCoinStartPoint());
            } while (percent < 1);

            yield return new WaitForSeconds(.4f);
            var scale = this.transform.localScale;
            this.transform.LeanScale(scale + Vector3.one * .2f, .4f).setEaseInExpo().setOnComplete(() =>
            {
                this.transform.LeanScale(scale, .4f).setEaseOutExpo().setOnComplete(OnComplete);
            });
        }
    }

    private void PlayCoinEffect(Transform pos)
    {
        var coin = CoinPool.Singleton.Get(CoinPool.CoinName.CoinStar).GetComponent<Coin>();
        coin.Init(false);
        coin.transform.position = pos.position;
        coin.transform.rotation = pos.rotation;

        coin.PlayCollectionEffect(this.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer != _playerLayer.value)
        {
            return;
        }

        var coinCollectable = other.transform.root.GetComponent<ICoinCollectable>();
        var currentCoin = coinCollectable.GetCurrentCoin();
        if (currentCoin >= _coinNeeded)
        {
            Fill(coinCollectable);
        }
    }

    private void OnComplete()
    {
        _unblockOnComplete.ForEach(item => item.SetActive(true));
        this.gameObject.SetActive(false);
    }
}