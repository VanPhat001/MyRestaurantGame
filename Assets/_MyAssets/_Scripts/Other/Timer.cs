using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float _timer = 0;
    TMP_Text _text;

    void Start()
    {
        _text = this.GetComponent<TMP_Text>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(_timer);
        // _text.text =  time .ToString(@"hh\:mm\:ss\:fff");
        _text.text =  time .ToString(@"hh\:mm\:ss");
    }
}