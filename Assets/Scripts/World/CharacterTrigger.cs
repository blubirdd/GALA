using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{
    Character character;
    private void Start()
    {
        character = GetComponent<Character>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TalkEvents.CharacterApproach(character);
        }
    }
}
