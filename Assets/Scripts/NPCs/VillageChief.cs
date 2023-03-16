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

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [Header("Quest")]
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;
    [SerializeField] private bool isTalked = false;

    [SerializeField] private bool isCompleted = false;

    [Header("Second Quest")]
    [SerializeField] private string secondQuest;

    [Header("Quest Marker")]
    public GameObject questMarker;
    private QuestNew quest { get; set; }
    public string npcName { get; set; }

    [Header("Subtle Dialogue")]
    [SerializeField] private SubtleDialogueTrigger _subtleDialogue;

//this is the closing quest if NPC is MAIN
    QuestTalkVillageChief2 closingQuest;
    Rigidbody rb;
    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
        npcName = _dialogue.name;

        if (isTalked == true)
        {
            DisableQuestMarker();
        }
        rb = GetComponent<Rigidbody>();

        
        if(isCompleted == true)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }

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

        else if (isCompleted == false)
        {
            if(Task.instance.tasksCompeleted.Contains(questType))
            {
                _dialogue.TriggerIsTalkedDialogue();

                TalkEvents.CharacterApproach(this);

                StartCoroutine(AcceptQuest());
            }
        }

        return true;
    }

    IEnumerator AcceptQuest()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);

        if(quests.TryGetComponent(out closingQuest))
        {
            if(closingQuest.questCompleted){
                isCompleted = true;

                this.gameObject.layer = LayerMask.NameToLayer("Default");
                _subtleDialogue.TriggerDialogue();
            }
        }

        if(isCompleted == false)
        {
            AssignQuest();
        }
        
    }

    void AssignQuest()
    {
        if (isTalked == false)
        {
            quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
            Debug.Log(this + "Quest New Assigned");
            
            DisableQuestMarker();
            
            isTalked = true;

            return;
        }

        if(isTalked == true && secondQuest != null)
        {
            quest = (QuestNew)quests.AddComponent(System.Type.GetType(secondQuest));

            DisableQuestMarker();
        }

    }

    void DisableQuestMarker()
    {
        questMarker.SetActive(false);

    }

    public void LoadData(GameData data)
    {
        data.NPCsTalked.TryGetValue(id, out isTalked);

        data.NPCsCompleted.TryGetValue(id, out isCompleted);
    }

    public void SaveData(GameData data)
    {
        if (data.NPCsTalked.ContainsKey(id))
        {
            data.NPCsTalked.Remove(id);
        }

        data.NPCsTalked.Add(id, isTalked);


        if (data.NPCsCompleted.ContainsKey(id))
        {
            data.NPCsCompleted.Remove(id);
        }

        data.NPCsCompleted.Add(id, isTalked);
    }


}
