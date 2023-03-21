using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    Character character;

    private void Start()
    {
        character = GetComponent<Character>();

    }
    public void OnTriggerEnter(Collider other)
    {

       if(other.gameObject.CompareTag("Water"))
        {
            TriggerQuest();
            Destroy(other);
            gameObject.SetActive(false);
        }
    }

    public void TriggerQuest()
    {
        TalkEvents.CharacterApproach(character);
    }
}
