using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    #region Singleton

    public static ParticleManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one particleManager of inventory found");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject puffParticle;


    public void SpawnPuffParticle(Vector3 location)
    {
       GameObject g = Instantiate(puffParticle, location, Quaternion.identity);
       Destroy(g, 3f);
    }
}
