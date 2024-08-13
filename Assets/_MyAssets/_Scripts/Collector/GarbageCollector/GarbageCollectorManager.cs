using UnityEngine;

public class GarbageCollectorManager : MonoBehaviour
{
    [SerializeField] GarbageCollectorMoverment _garbageCollectorMoverment;
    public GarbageCollectorMoverment GarbageCollectorMoverment => _garbageCollectorMoverment;

    [SerializeField] GarbageCollectorAnimation _garbageCollectorAnimation;
    public GarbageCollectorAnimation Anim => _garbageCollectorAnimation;

}