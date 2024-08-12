using System.Collections;
using UnityEngine;

public class DisableAfter : MonoBehaviour
{
    [SerializeField] private float _sec;

    void OnEnable()
    {
        StartCoroutine(Excute());
    }

    IEnumerator Excute()
    {
        yield return new WaitForSeconds(_sec);
        this.gameObject.SetActive(false);
    }
}