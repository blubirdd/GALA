using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNPC : MonoBehaviour, ICharacter, IInteractable
{
    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    [SerializeField] private DialogueTrigger _dialogue;

    public string InteractionPrompt => _prompt;
    public Sprite icon => _icon;


    [Header("Quest Marker")]
    public GameObject questMarker;



    public string npcName { get; set; }

    void Start()
    {
        npcName = _dialogue.name;
    }

    public bool Interact(Interactor interactor)
    {
        //EVENT TRIGGER
        TalkEvents.CharacterApproach(this);
        _dialogue.TriggerDialogue();

        return true;
    }


    void DisableQuestMarker()
    {
        questMarker.SetActive(false);

    }

}
