using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SandwichOven : MonoBehaviour
{
    [SerializeField] private TMP_Text _statusText;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _sandwichPoint;
    [SerializeField] private Transform _spanwPoint;
    [SerializeField] private float _heightOffset = .2f;
    [SerializeField] private int _capacity = 0;
    [SerializeField] private LayerMask _playerLayer;
    private readonly float _finishTime = 4f;
    private float _timer = 0;

    private readonly int OpenState = Animator.StringToHash("open");

    void Start()
    {
        UpdateStatusText(false);
    }

    void Update()
    {
        if (IsFull())
        {
            UpdateStatusText(true);
            return;
        }

        UpdateStatusText(false);
        Making();
    }

    void Making()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _timer = _finishTime;
            StartCoroutine(MakeDoneCoroutine());
        }
    }

    IEnumerator MakeDoneCoroutine()
    {
        SetOpenState(true);
        var sandwich = FoodPool.Singleton.Get(FoodPool.FoodName.Sandwich).transform;
        sandwich.position = _sandwichPoint.position;
        yield return new WaitForSeconds(1);

        sandwich.LeanMove(_spanwPoint.position + Vector3.up * _heightOffset * _spanwPoint.childCount, .4f).setEaseInExpo();
        yield return new WaitForSeconds(.4f);

        sandwich.SetParent(_spanwPoint);
        SetOpenState(false);
    }

    bool IsFull()
    {
        return _spanwPoint.childCount >= _capacity;
    }


    public void SetOpenState(bool value)
    {
        _animator.SetBool(OpenState, value);
    }

    public void UpdateStatusText(bool isFull)
    {
        _statusText.text = isFull ? "MAX" : "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer != _playerLayer.value)
        {
            return;
        }

        SendItem();
    }

    List<GameObject> PopSandwich(int number)
    {
        var list = new List<GameObject>();
        int lastIndex = _spanwPoint.childCount - 1;
        for (int i = 0; i < number; i++)
        {
            var index = lastIndex - i;
            list.Add(_spanwPoint.GetChild(index).gameObject);
        }
        return list;
    }

    void SendItem()
    {
        var itemManager = PlayerManager.Singleton.ItemManager;
        var number = itemManager.NumberItemGet(ItemName.Sandwich);
        number = Mathf.Min(number, _spanwPoint.childCount);
        var goList = PopSandwich(number);
        itemManager.CollectItem(ItemName.Sandwich, goList);
    }
}
