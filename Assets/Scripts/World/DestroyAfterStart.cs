using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterStart : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration = 5f;
    void Start()
    {
        StartCoroutine(WaitForSecondsAfterStart());
    }

    IEnumerator WaitForSecondsAfterStart()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
