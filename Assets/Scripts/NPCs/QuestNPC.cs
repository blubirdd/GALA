using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour, ICharacter, IInteractable, IDataPersistence
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
    [SerializeField] private GameObject questManager;
    [SerializeField] private string prerequisiteQuest;
    [SerializeField] private string questName;
    [SerializeField] private bool isTalked = false;

    [SerializeField] private bool isCompleted = false;


    [Header("Quest Marker")]
    public GameObject questMarker;
    private QuestNew quest { get; set; }
    public string npcName { get; set; }

    //this is the closing quest if NPC is MAIN
    [SerializeField] private string closingQuestID;
    Task task;

    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;

        npcName = id;

        if (isTalked == true)
        {
            DisableQuestMarker();
        }



        if (isCompleted == true)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        else
        {
            this.gameObject.layer = LayerMask.NameToLayer("Interactable");
        }


    }

    public bool Interact(Interactor interactor)
    {
        if (Task.instance.tasksCompeleted.Contains(prerequisiteQuest))
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
                if (Task.instance.tasksCompeleted.Contains(questName))
                {
                    _dialogue.TriggerIsTalkedDialogue();

                    TalkEvents.CharacterApproach(this);

                    StartCoroutine(AcceptQuest());

                }

                else
                {
                    Debug.Log("NPCs task is not yet completed. Please finish the task");
                }
            }

        }

        else
        {
            Debug.Log(this + "is unavailable right now");
        }


        return true;
    }

    IEnumerator AcceptQuest()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);

        //look for the closing quest of the NPC

        if (questManager.TryGetComponent(out task))
        {
            if (closingQuestID != null)
            {
                if (task.tasksCompeleted.Contains(closingQuestID))
                {
                    isCompleted = true;

                    this.gameObject.layer = LayerMask.NameToLayer("Default");
                }
            }
        }

        if (isCompleted == false)
        {
            AssignQuest();
        }
    }

    void AssignQuest()
    {
        if (isTalked == false)
        {
            quest = (QuestNew)questManager.AddComponent(System.Type.GetType(questName));
            Debug.Log(this + "Quest New Assigned");

            DisableQuestMarker();

            isTalked = true;

            return;
        }

        if(isTalked == true)
        {
            quest = (QuestNew)questManager.AddComponent(System.Type.GetType(closingQuestID));
            Debug.Log(this + "Quest New Assigned");
    

            isCompleted = true;
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }

    }

    void DisableQuestMarker()
    {
        if(questMarker != null)
        {
           questMarker.SetActive(false);
        }


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
