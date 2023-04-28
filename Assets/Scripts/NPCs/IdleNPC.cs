using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNPC : MonoBehaviour, ICharacter, IInteractable, IDataPersistence
{
    [SerializeField] private string id;
    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    [SerializeField] private DialogueTrigger _dialogue;
    [SerializeField] private DialogueTrigger isTalkedDialogue;
    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [Header("Quest Marker")]
    public GameObject questMarker;


    [Header("Save and load")]
    [SerializeField] private bool isTalked = false;

    public string npcName { get; set; }

    [Header("Idle Animation")]
    public string animationName;

    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
        npcName = id;


        if(isTalked)
        {
            DisableQuestMarker();
        }

    }

    public bool Interact(Interactor interactor)
    {
        //EVENT TRIGGER
        TalkEvents.CharacterApproach(this);

        if (!isTalked)
        {
            if (isTalkedDialogue == null)
            {
                _dialogue.TriggerDialogue();

                PositionPlayer(interactor);
            }

            else
            {
                isTalkedDialogue.TriggerDialogue();
                PositionPlayer(interactor);
            }
        }
        else
        {
            if(isTalkedDialogue != null)
            {
               isTalkedDialogue.TriggerDialogue();
               PositionPlayer(interactor);
            }
           
        }

        isTalked = true;

        return true;
    }

    public void PositionPlayer(Interactor interactor)
    {
        interactor.gameObject.SetActive(false);
        interactor.transform.position = transform.position + transform.forward * 2;
        interactor.transform.LookAt(transform);
        interactor.gameObject.SetActive(true);
    }


    void DisableQuestMarker()
    {
        questMarker.SetActive(false);

    }


    public void SaveData(GameData data)
    {
        if (data.NPCsTalked.ContainsKey(id))
        {
            data.NPCsTalked.Remove(id);
        }

        data.NPCsTalked.Add(id, isTalked);

    }

    public void LoadData(GameData data)
    {
        data.NPCsTalked.TryGetValue(id, out isTalked);
    }

}
