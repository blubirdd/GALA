using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNPC : MonoBehaviour, ICharacter, IInteractable
{
    [Header("Unique ID")]
    public string id;

    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    [SerializeField] private DialogueTrigger _dialogue;
    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [Header("Quest")]
    [SerializeField] private GameObject questManager;
    [SerializeField] private string prerequisiteQuest;
    [SerializeField] private string questName;
    //[SerializeField] private bool isTalked = false;

    //[SerializeField] private bool isCompleted = false;

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
    [SerializeField] private GameObject objectToActivate;

    Task task;

    void Start()
    {
        animator = GetComponent<Animator>();
        navigation = GetComponent<AnimalNav>();

        InteractionPrompt = _prompt;
        icon = _icon;

        npcName = id;

        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("DemoQuest1"));
    }

    public bool Interact(Interactor interactor)
    {
       
        // Get the direction towards the interactor
        Vector3 direction = interactor.transform.position - transform.position;

        // Ignore rotation on x and z axes
        direction.y = 0;

        // Calculate the rotation towards the interactor
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Apply the rotation
        transform.rotation = lookRotation;

        //trigger dialogue
        TriggerDialogueQuest();

      return true;
    }

    public void TriggerDialogueQuest()
    {
        //trigger dialogue

            _dialogue.TriggerDialogue();

            //EVENT TRIGGER
            TalkEvents.CharacterApproach(this);

            StartCoroutine(AcceptQuest());

    }

    IEnumerator AcceptQuest()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);

            AssignQuest();
    }

    void AssignQuest()
    {
            //quest = (QuestNew)questManager.AddComponent(System.Type.GetType(questName));
            //Debug.Log(this + "Quest New Assigned");
    }


    public void LoadData(GameData data)
    {
        //data.NPCsTalked.TryGetValue(id, out isTalked);

        //data.NPCsCompleted.TryGetValue(id, out isCompleted);
    }

}
