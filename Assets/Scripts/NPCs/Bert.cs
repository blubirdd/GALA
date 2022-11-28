using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bert : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    [SerializeField] private DialogueTrigger _dialogue;

    public string InteractionPrompt => _prompt;


    [SerializeField] private GameObject quests;

    [SerializeField] private string questType;

    [SerializeField] private bool isTalked = false;
    private QuestNew quest { get; set; }

    void Start()
    {
       
    }
    public bool Interact(Interactor interactor)
    {
        //trigger dialogue
        _dialogue.TriggerDialogue();

        StartCoroutine(AcceptQuest());
        return true;
    }

    IEnumerator AcceptQuest()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        AssignQuest();
        

    }

    void AssignQuest()
    {
        if(isTalked == false)
        {
        quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
        Debug.Log(this + "Quest New Assigned");
        transform.Find("questMarker").gameObject.SetActive(false);

            isTalked = true;
        }

        else
        {
            Debug.Log("Already talked to this NPC");
        }
    }


}
