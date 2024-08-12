using UnityEngine;

public class Grabage : MonoBehaviour, IReleaseable
{
    public void Release()
    {
        this.transform.parent = null;
        Destroy(this.gameObject);
    }
}