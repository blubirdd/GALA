using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnDisable()
    {
        StartCoroutine(EnableWithDelay(10));
    }

    IEnumerator EnableWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        this.gameObject.SetActive(true);

    }
}
