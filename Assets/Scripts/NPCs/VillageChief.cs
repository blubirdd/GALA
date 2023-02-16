using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageChief : MonoBehaviour, ICharacter, IDataPersistence, IInteractable
{
    [Header("Unique ID")]
    public string id;

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

    }

    public void LoadData(GameData data)
    {
        data.NPCsTalked.TryGetValue(id, out isTalked);
    }

    public void SaveData(GameData data)
    {
        if (data.NPCsTalked.ContainsKey(id))
        {
            data.NPCsTalked.Remove(id);
        }

        data.NPCsTalked.Add(id, isTalked);
    }


}
