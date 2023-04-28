using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplashCollide : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Collidied with water");
            ParticleManager.instance.SpawnWaterSplashParticle(transform.position);
        }
    }
}
