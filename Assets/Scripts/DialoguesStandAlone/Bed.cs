using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IDataPersistence
{
    [SerializeField] private DialogueTrigger _dialogue;
    public bool triggerStartDialogue;


    //yes this is a quest giver
    private QuestNew quest { get; set; }

    [Header("Quest Manager")]
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;

    // Start is called before the first frame update
    void Start()
    {
        if (triggerStartDialogue)
        {
            StartCoroutine(WaitForDialogueToTrigger());
        }


    }

    IEnumerator WaitForDialogueToTrigger()
    {
        yield return new WaitForSeconds(1f);
        _dialogue.TriggerDialogue();
        triggerStartDialogue = false;

        StartCoroutine(AcceptQuest());

    }

    IEnumerator AcceptQuest()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        AssignQuest();
        yield return new WaitForSeconds(1f);
        UIManager.instance.OpenCloseTutorialPrompt();
    }

    void AssignQuest()
    {
        Debug.Log("Assigning first quest...");
        quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
        Debug.Log(this + "Quest New Assigned");

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