using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneUIManager : MonoBehaviour
{
    public static GameSceneUIManager Singleton { get; private set; }

    [SerializeField] private Joystick _joystick;
    public Joystick Joystick => _joystick;


    [SerializeField] private TMP_Text _coinText;




    void Awake()
    {
        Singleton = this;
    }

    public void SetCoinText(float value)
    {
        _coinText.text = Mathf.Ceil(value).ToString();
    }
}