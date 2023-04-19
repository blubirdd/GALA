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
    [SerializeField] private SubtleDialogueTrigger _completeTasksubtleDialogue;
    [SerializeField] private SubtleDialogueTrigger _notAvailablesubtleDialogue;
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


    [Header("Action Animations")]
    Animator animator;
    AnimalNav navigation;
    public Transform targetTransform;
    public Transform targetTransform2;
    [Header("Activate something on target reached")]
    [SerializeField]  private GameObject objectToActivate;

    Task task;

    void Start()
    {
        animator = GetComponent<Animator>();
        navigation = GetComponent<AnimalNav>();

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
        if (prerequisiteQuest != "None" && Task.instance.tasksCompeleted.Contains(prerequisiteQuest))
        {
            //trigger dialogue
            TriggerDialogueQuest();

        }

        else if(prerequisiteQuest !="None")
        {
            if(_notAvailablesubtleDialogue != null)
            {
                _notAvailablesubtleDialogue.TriggerDialogue();
            }

            Debug.Log(this + "is unavailable right now");
        }

        else if(prerequisiteQuest == "None")
        {
            TriggerDialogueQuest();
        }


        return true;
    }

    public void TriggerDialogueQuest()
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
                if (_completeTasksubtleDialogue != null)
                {
                    _completeTasksubtleDialogue.TriggerDialogue();
                }
                Debug.Log("NPCs task is not yet completed. Please finish the task");

            }
        }
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

            if(targetTransform != null)
            {
                navigation.TargetLocation(targetTransform);
                animator.SetBool("Run", true);
                StartCoroutine(WaitUntilTargetIsReached(false));

            }


            return;
        }

        if(isTalked == true)
        {
            StartCoroutine(WaitForNotification());
            IEnumerator WaitForNotification()
            {
                yield return new WaitForSeconds(1f);
                quest = (QuestNew)questManager.AddComponent(System.Type.GetType(closingQuestID));
                Debug.Log(this + "Quest New Assigned");

            }
           

            if (targetTransform2 != null)
            {
                navigation.TargetLocation(targetTransform2);
                animator.SetBool("Run", true);
                StartCoroutine(WaitUntilTargetIsReached(true));
            }

            isCompleted = true;
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }

    }

    IEnumerator WaitUntilTargetIsReached(bool destroyAfter)
    {
        yield return new WaitUntil(() => navigation.targetReached == true);
        animator.SetBool("Run", false);

        if(objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        if (destroyAfter)
        {
            Destroy(gameObject);
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
        //data.NPCsTalked.TryGetValue(id, out isTalked);

        //data.NPCsCompleted.TryGetValue(id, out isCompleted);
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

        data.NPCsCompleted.Add(id, isCompleted);
    }
}
