using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{
    public static SeatManager Singleton { get; private set; }

    [SerializeField] private List<Seat> _seats;



    void Awake()
    {
        Singleton = this;
    }

    public Seat FindEmptySeat()
    {
        return _seats.Find(item => !item.IsUsed);
    }
}