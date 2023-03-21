using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Task task;

    [Header("Grasslands")]
    [SerializeField] private GameObject injuredTamaraw;
    [SerializeField] private GameObject curableTamaraw;
    [SerializeField] private GameObject wildlifeSpecialist;

    [Header("River")]
    public SubtleDialogueTrigger afterPhotoForestTurtle;
    public GameObject FireQuestTriggerCollider;
    public GameObject FireQuest;

    [Header("River Dialogues")]
    public SubtleDialogueTrigger afterFireExtinguishedDialogue;

    [Header("Grasslands Tamaraw Prefab")]
    public GameObject tamarawPrefab;



    private void Start()
    {
        //set to falses
        

        GameEvents.instance.onQuestAcceptedNotification += QuestCheck;
        GameEvents.instance.onQuestCompleted += QuestCompleteCheck;


        for (int i = 0; i < task.tasksCompeleted.Count; i++)
        {
            //scene setup
            if(task.tasksCompeleted[i] == "QuestTalkWildlifeSpecialist2")
            {
                Destroy(injuredTamaraw);
                curableTamaraw.SetActive(true);
            }

            //disable for debug purposes
            if (task.tasksCompeleted[i] == "QuestUseMedkit")
            {
                curableTamaraw.SetActive(false);
                wildlifeSpecialist.SetActive(false);
            }

            if (task.tasksCompeleted[i] == "QuestPhotographForestTurtle")
            {
                FireQuestTriggerCollider.SetActive(true);

            }

            else
            {
                FireQuestTriggerCollider.SetActive(false);
            }


        }
    }

    public void QuestCheck(string quest)
    {
        //QuestUseMedkit2
        if(quest == "Curing the Tamaraws")
        {
            Destroy(wildlifeSpecialist);
        }
    }

    public void QuestCompleteCheck(string quest)
    {
        //QuestCollectTamarawTracks
        if(quest == "Tracking the tamaraw")
        {
            injuredTamaraw.SetActive(true);
            curableTamaraw.SetActive(false);
            wildlifeSpecialist.SetActive(false);
            Debug.Log("Completed " + quest);
        }

        if(quest == "Tamaraw Rescue")
        {
            injuredTamaraw.SetActive(false);
            curableTamaraw.SetActive(true);
            wildlifeSpecialist.SetActive(true);
        }

        if(quest == "Heal the Tamaraw")
        {
            Destroy(curableTamaraw);
            Instantiate(tamarawPrefab, curableTamaraw.transform.position, Quaternion.identity);
        }

        //QuestPhotographForestTurtle
        if(quest =="Forest Turtle Adventure")
        {
            FireQuestTriggerCollider.SetActive(true);

            StartCoroutine(WaitForCamera());
            IEnumerator WaitForCamera()
            {
                yield return new WaitUntil(() => UIManager.instance.inGameCameraCanvas.activeSelf == false);
                afterPhotoForestTurtle.TriggerDialogue();
            }
           
        }

        if(quest == "Help extinguish the fire")
        {
            afterFireExtinguishedDialogue.TriggerDialogue();
        }


    }


}
