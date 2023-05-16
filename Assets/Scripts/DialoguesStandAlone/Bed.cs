using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IDataPersistence
{
    [SerializeField] private DialogueTrigger _dialogue;
    public bool triggerStartDialogue;
    public GameObject eagleCutscene;


    //yes this is a quest giver
    private QuestNew quest { get; set; }

    [Header("Quest Manager")]
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;

    public GameObject skipIntroButton;

    // Start is called before the first frame update

    // waypoint
    //WaypointMarker waypointMarker;
    void Start()
    {
        if (triggerStartDialogue)
        {
            skipIntroButton.SetActive(true);
            UIManager.instance.DisablePlayerMovement();
            eagleCutscene.SetActive(true);
            StartCoroutine(WaitForDialogueToTrigger());

        }

        else
        {
            skipIntroButton.SetActive(false);
        }

       // waypointMarker = GetComponent<WaypointMarker>();

    }

    public void SkipIntro()
    {
        StopAllCoroutines();
        eagleCutscene.SetActive(false);
        _dialogue.TriggerDialogue();
        triggerStartDialogue = false;
        skipIntroButton.SetActive(false);
        StartCoroutine(AcceptQuest());


        CinemachineManager.instance._cams[0].SetActive(true);
        CinemachineManager.instance._cams[7].SetActive(false);

    }

    IEnumerator WaitForDialogueToTrigger()
    {
        //wait for cutscene
        yield return new WaitForSeconds(10.5f);

        skipIntroButton.SetActive(false);
        _dialogue.TriggerDialogue();
        triggerStartDialogue = false;

        StartCoroutine(AcceptQuest());

    }

    IEnumerator AcceptQuest()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
       
        //UIManager.instance.OpenCloseTutorialPrompt();

        AssignQuest();
    }

    void AssignQuest()
    {
        Debug.Log("Assigning first quest...");
        quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
        Debug.Log(this + "Quest New Assigned");

        //waypointMarker.SpawnWaypointMarker();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        triggerStartDialogue = data.triggerStartDialogue;
    }

    public void SaveData(GameData data)
    {
        data.triggerStartDialogue = triggerStartDialogue;
    }
}
