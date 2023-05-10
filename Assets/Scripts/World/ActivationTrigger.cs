using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivationTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var item in objectsToActivate)
            {
                item.SetActive(true);
            }

            foreach (var item in objectsToDeactivate)
            {
                item.SetActive(false);
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
