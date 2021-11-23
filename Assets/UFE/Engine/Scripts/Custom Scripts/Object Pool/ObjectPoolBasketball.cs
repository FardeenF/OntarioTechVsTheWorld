using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBasketball : MonoBehaviour
{
    public float time = 20.0f;

    private void Update()
    {
        transform.Rotate(100.0f * Time.deltaTime, 0.0f, 0.0f, Space.Self);
    }

    void OnEnable()
    {
        StartCoroutine(DestroyBasketball());
    }

    IEnumerator DestroyBasketball()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
