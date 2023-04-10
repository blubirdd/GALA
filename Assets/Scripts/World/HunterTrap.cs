using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterTrap : MonoBehaviour
{
    Character character;
    public Item item;

    private void Start()
    {
        character = GetComponent<Character>();

    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Rock"))
        {
            TriggerQuest();
            Destroy(other.gameObject);

            //trigger particle
            ParticleManager.instance.SpawnPuffParticle(transform.position);
            PopupWindow.instance.AddToQueue(item);
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void TriggerQuest()
    {
        TalkEvents.CharacterApproach(character);
    }
}
