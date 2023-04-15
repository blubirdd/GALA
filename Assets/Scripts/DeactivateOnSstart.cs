using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnSstart : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(WaitForFewSeconds());
    }

    IEnumerator WaitForFewSeconds()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }
}
