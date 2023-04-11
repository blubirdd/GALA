using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.onQuestAcceptedNotification += VillageQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += VillageQuestCompleteCheck;
    }

    public void VillageQuestAcceptCheck(string questName) 
    {
        //QuestIntroPart1
        if(questName == "The mysterious person")
        {
            StartCoroutine(WaitUntilDialogueEnd(0, 0.3f));
        }

        //QuestTalkVillageChief
        if (questName == "Find out about the Village")
        {
            StartCoroutine(WaitUntilDialogueEnd(1, 0.3f));
        }

        //QuestPhotoChicken
        if(questName == "Animals in Antrophogenic Biome")
        {
            StartCoroutine(WaitUntilDialogueEnd(2, 3f));
        }
    }

    public void VillageQuestCompleteCheck(string questName)
    {

    }
    IEnumerator WaitUntilDialogueEnd(int tutorialID , float delay)
    {
        //yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        yield return new WaitForSeconds(delay);
        TutorialUI.instance.EnableTutorial(tutorialID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
