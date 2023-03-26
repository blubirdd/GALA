using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrandedDugong : MonoBehaviour
{
    Character character;

    private void Start()
    {
        character = GetComponent<Character>();

    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Water"))
        {
            TriggerQuest();
            Destroy(other.gameObject);

            //trigger particle
            ParticleManager.instance.SpawnWaterSplashParticle(transform.position);
        }
    }

    public void TriggerQuest()
    {
        TalkEvents.CharacterApproach(character);
    }
}
