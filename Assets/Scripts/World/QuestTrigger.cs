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
            other.gameObject.tag = "Untagged";


            if (destroyOnCollide)
            {
                Destroy(other.gameObject);
            }


            //trigger particle
            ParticleManager.instance.SpawnPuffParticle(transform.position);

            if (deactivateTriggerOnCollide)
            {
                gameObject.SetActive(false);
            }

        }
    }

    public void TriggerQuest()
    {
        TalkEvents.CharacterApproach(character);
        
        Debug.Log("Detected Forest Turtle");
    }
}
