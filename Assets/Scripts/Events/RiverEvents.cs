using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject injuredTurtle;
    public GameObject fireQuest;

    [Header("Dialogues")]
    public SubtleDialogueTrigger dialogueToGoToBeach;
    void Start()
    {
        injuredTurtle.SetActive(false);
        GameEvents.instance.onQuestCompleted += RiverQuestCompleteCheck;

    }

    public void RiverQuestCompleteCheck(string questName)
    {
        //QuestTalkDayTent
        if(questName == "Rest in the camp until morning")
        {
            injuredTurtle.SetActive(true);

            fireQuest.SetActive(false);
        }

        //QuestCollectContaminatedBarrel
        if (questName == "Cleaning the river")
        {
            dialogueToGoToBeach.TriggerDialogue();
        }
    }

    private void OnDisable()
    {
        GameEvents.instance.onQuestCompleted -= RiverQuestCompleteCheck;
    }
}
