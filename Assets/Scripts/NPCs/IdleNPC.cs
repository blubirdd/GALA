using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNPC : MonoBehaviour, ICharacter, IInteractable
{
    [SerializeField] private string id;
    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    [SerializeField] private DialogueTrigger _dialogue;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [Header("Quest Marker")]
    public GameObject questMarker;



    public string npcName { get; set; }

    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
        npcName = id;
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
