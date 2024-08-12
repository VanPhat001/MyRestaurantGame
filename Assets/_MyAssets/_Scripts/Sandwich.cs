using UnityEngine;

public class Sandwich : MonoBehaviour, IReleaseable
{
    public void Release()
    {
        FoodPool.Singleton.Release(this.gameObject);
    }
}