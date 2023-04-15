using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEgg : MonoBehaviour
{
    public float damage;
    private HealthBar pHealth;

    void Start()
    {
        // Find the object with the HealthBar component and assign it to pHealth
        pHealth = FindObjectOfType<HealthBar>();
        if (pHealth == null)
        {
            Debug.LogError("No HealthBar found in the scene!");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Egg"))
        {
            pHealth.health -= damage;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Egg"))
    //    {
    //        Debug.Log("TEST HELLO 2");
    //        pHealth.health -= damage;
    //    }
    //}
}