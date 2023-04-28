using BrokenVector.LowPolyFencePack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public Door door;
    public GameObject[] itemsToCollect;

    public DoorController doorController;
    void Start()
    {
        GameEvents.instance.onQuestAcceptedNotification += VillageQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += VillageQuestCompleteCheck;

        if (Task.instance.tasksCompeleted.Contains("QuestIntroPart1"))
        {
            foreach (var item in itemsToCollect)
            {
                item.SetActive(true);
            }
        }

        else
        {
            foreach (var item in itemsToCollect)
            {
                item.SetActive(false);
            }
        }

        if (Task.instance.tasksCompeleted.Contains("QuestIntroPart2"))
        {
            door.animator.SetBool("isOpen", true);
            foreach (var item in itemsToCollect)
            {
                Destroy(item);
            }
        }
    }

    public void VillageQuestAcceptCheck(string questName) 
    {
        //QuestIntroPart1
        if(questName == "The mysterious person")
        {
            StartCoroutine(WaitUntilDialogueEnd(0, 2f));
        }

        if(questName == "Preparation for Adventure")
        {
            foreach (var item in itemsToCollect)
            {
                item.SetActive(true);
            }
        }


        //QuestTalkVillageChief
        if (questName == "Find out about the Village")
        {
            //StartCoroutine(WaitUntilDialogueEnd(1, 2f));
        }

        //QuestPhotoChicken
        if(questName == "Photograph a Red junglefowl")
        {
            StartCoroutine(WaitUntilDialogueEnd(2, 2f));
        }

        //QuestFeedChicken
        if(questName == "Feed the Red junglefowls")
        {
            StartCoroutine(WaitForDialogue());
            IEnumerator WaitForDialogue()
            {
                yield return new WaitForSeconds(1f);
                TutorialUI.instance.EnableTutorial(5);
            }

        }

    }

    public void VillageQuestCompleteCheck(string questName)
    {
        //QuestIntroPart2
        if (questName == "Preparation for Adventure")
        {
            //QuestIntroPart2
            door.animator.SetBool("isOpen", true);
        }

        //QuestTakeVillageQuiz
        if(questName == "Take and pass the quiz")
        {
            doorController.ToggleDoor();
        }
    }
    IEnumerator WaitUntilDialogueEnd(int tutorialID , float delay)
    {

        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        yield return new WaitForEndOfFrame();
        UIManager.instance.DisablePlayerMovement();
        yield return new WaitForSeconds(delay);
        TutorialUI.instance.EnableTutorial(tutorialID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
