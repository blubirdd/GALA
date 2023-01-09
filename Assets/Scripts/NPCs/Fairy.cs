using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
public class Fairy : MonoBehaviour,  IInteractable, ICharacter
{
    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    [SerializeField] private DialogueTrigger _dialogue;
    [SerializeField] private DialogueTrigger _isTalkedDialogue;

    public string InteractionPrompt => _prompt;
    public Sprite icon => _icon;

    [Header("Quest")]
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;
    [SerializeField] private bool isTalked = false;

    [Header("Quest Marker")]
    public GameObject questMarker;
    private QuestNew quest { get; set; }
    public string npcName { get; set; }

    Rigidbody rb;
    void Start()
    {
        npcName = _dialogue.name;

        if (isTalked == true)
        {
            DisableQuestMarker();
        }
        rb = GetComponent<Rigidbody>();
    }


    public bool Interact(Interactor interactor)
    {
        //trigger dialogue
        if (isTalked == false)
        {
            _dialogue.TriggerDialogue();

            //EVENT TRIGGER
            TalkEvents.CharacterApproach(this);

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
        questMarker.SetActive(false);

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return null;
    }
}
