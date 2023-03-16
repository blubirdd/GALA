using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bert : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    [SerializeField] private DialogueTrigger _dialogue;
    [SerializeField] private DialogueTrigger _isTalkedDialogue;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [SerializeField] private GameObject quests;

    [SerializeField] private string questType;

    [SerializeField] private bool isTalked = false;

    
    private QuestNew quest { get; set; }

    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;

        if (isTalked == true)
        {
            DisableQuestMarker();
        }
    }

    
    public bool Interact(Interactor interactor)
    {
        //trigger dialogue
        if(isTalked == false)
        {
            _dialogue.TriggerDialogue();

            StartCoroutine(AcceptQuest());
        }

        else
        {
            _isTalkedDialogue.TriggerIsTalkedDialogue();
            Debug.Log("Already talked to this NPC");
        }

        return true;
    }

    IEnumerator AcceptQuest()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        AssignQuest();

    }

    void AssignQuest()
    {
        if (isTalked == false)
        {
            quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
            Debug.Log(this + "Quest New Assigned");
            isTalked = true;
        }
        DisableQuestMarker();
    }

    void DisableQuestMarker()
    {
        transform.Find("questMarker").gameObject.SetActive(false);
    }


}
