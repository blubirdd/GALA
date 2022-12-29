using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour,  IInteractable
{

    [SerializeField] private string _prompt;
    [SerializeField] private DialogueTrigger _dialogue;

    public string InteractionPrompt => _prompt;

    [SerializeField] private Quest11 quest;




    void Start()
    {
       

    }

    public bool Interact(Interactor interactor)
    {
        //trigger dialogue
        _dialogue.TriggerDialogue();
     

        //setup
        StartCoroutine(AcceptQuest());
        
        return true;
    }

    void DisableQuestMarker()
    {
        transform.Find("questMarker").gameObject.SetActive(false);
    }



    IEnumerator AcceptQuest()
    {
        // yield return new WaitUntil(() => dialogueSystem.dialogueEnded == true);
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);

        //quest.AcceptQuest();
        DisableQuestMarker();
        Debug.Log("Quest Accepted");

    }
}
