using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    Character character;
    [SerializeField] private string tagToDetect;
    [SerializeField] private bool destroyOnCollide = false;
    [SerializeField] private bool deactivateTriggerOnCollide = false;

    private void Start()
    {
        character = GetComponent<Character>();

    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(tagToDetect))
        {
            TriggerQuest();
            


            if (destroyOnCollide)
            {
                Destroy(other.gameObject);
            }


            //trigger particle
            ParticleManager.instance.SpawnPuffParticle(other.transform.position);

            if (deactivateTriggerOnCollide)
            {
                gameObject.SetActive(false);
            }
            other.gameObject.tag = "Untagged";

        }
    }

    public void TriggerQuest()
    {
        TalkEvents.CharacterApproach(character);
        Debug.Log("TRIGGER Detected" + character.name);

    }
}
