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
    public GameObject waterSplashParticle;
    public GameObject chestOpenParticle;

    public void SpawnChestOpenParticle(Vector3 location)
    {
        SoundManager.instance.PlaySoundFromClips(13);


        StartCoroutine(WaitAnotherToSpawn());
        IEnumerator WaitAnotherToSpawn()
        {
            GameObject g = Instantiate(chestOpenParticle, location, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            GameObject h = Instantiate(chestOpenParticle, location, Quaternion.identity);
            //yield return new WaitForSeconds(0.5f);
            //GameObject j = Instantiate(chestOpenParticle, location, Quaternion.identity);
        }

    }
    public void SpawnPuffParticle(Vector3 location)
    {
        SoundManager.instance.PlaySoundFromClips(10);
       GameObject g = Instantiate(puffParticle, location, Quaternion.identity);
       //Destroy(g, 3f);
    }

    public void SpawnWaterSplashParticle(Vector3 location)
    {
        GameObject g = Instantiate(waterSplashParticle, location, Quaternion.identity);
        //Destroy(g, 3f);
    }
}
