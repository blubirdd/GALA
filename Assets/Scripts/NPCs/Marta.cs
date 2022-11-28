using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marta : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    [SerializeField] private DialogueTrigger _dialogue;

    public string InteractionPrompt => _prompt;


    [SerializeField] private GameObject quests;

    [SerializeField] private string questType;

    

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
        quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
        Debug.Log("Quest New Assigned");
        
    }

    void RemoveQuest()
    {
       
    }

}
