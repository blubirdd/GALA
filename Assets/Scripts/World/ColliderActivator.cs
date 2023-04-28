using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivator : MonoBehaviour
{
    [SerializeField] private GameObject objectToEnable;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToEnable.SetActive(true);
        }
    }
}
