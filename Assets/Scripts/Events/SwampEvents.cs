using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampEvents : MonoBehaviour
{
    public GameObject crocodileQuestAnimals;
    public GameObject netItem;

    public GameObject crocodileRelocateTrigger;

    [Header("Dialouges")]
    public SubtleDialogueTrigger containCrocsSubtleDialogue;
    
    void Start()
    {
        GameEvents.instance.onQuestAcceptedNotification += SwampQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += SwampQuestCompleteCheck;
    }

    public void SwampQuestAcceptCheck(string questName)
    {
        //QuestContainCrocodiles
        if(questName == "Contain and capture the crocodiles")
        {
            containCrocsSubtleDialogue.TriggerDialogue();
            netItem.GetComponent<Outline>().enabled = true;
        }

    }

    public void SwampQuestCompleteCheck(string questName)
    {
        //QuestTalkSwampResident
        if (questName == "Swamp and Marshes Wetlands")
        {
            crocodileQuestAnimals.SetActive(true);
        }

        if(questName == "Releasing the crocodiles")
        {
            crocodileRelocateTrigger.SetActive(false);
        }

    }
}
