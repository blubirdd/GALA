using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulCollider : MonoBehaviour
{
    [SerializeField] private Transform respawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);

            Player.instance.EnableSwampLakeUI();

        }
    }
}
