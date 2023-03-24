using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSecondsAfterStart());
    }

    IEnumerator WaitForSecondsAfterStart()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
