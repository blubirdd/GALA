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

    //[Header("Grasslands Tamaraw Prefab")]
    //public GameObject tamarawPrefab;

    [Header("River")]
    public SubtleDialogueTrigger afterPhotoForestTurtle;
    public GameObject FireQuestTriggerCollider;
    public GameObject FireQuest;

    [Header("River Aftermath")]
    public GameObject characterLawrence;
    public GameObject characterLawrenceAftermath;

    

    [Header("River Dialogues")]
    public SubtleDialogueTrigger afterFireExtinguishedDialogue;


    [Header("Waypoint ")]
    public Transform campWaypoint;
    private GameObject _waypoint;

    private void Start()
    {
        //set to falses


        GameEvents.instance.onQuestAcceptedNotification += QuestCheck;
        GameEvents.instance.onQuestCompleted += QuestCompleteCheck;


        for (int i = 0; i < task.tasksCompeleted.Count; i++)
        {
            //scene setup
            //if (task.tasksCompeleted[i] == "QuestTalkWildlifeSpecialist2")
            //{
            //    Destroy(injuredTamaraw);
            //    curableTamaraw.SetActive(true);
            //}

            ////disable for debug purposes
            //if (task.tasksCompeleted[i] == "QuestUseMedkit")
            //{
            //    curableTamaraw.SetActive(false);
            //    wildlifeSpecialist.SetActive(false);
            //}

            ///
            if (task.tasksCompeleted[i] == "QuestPhotographForestTurtle")
            {
                FireQuestTriggerCollider.SetActive(true);

            }

            else
            {
                FireQuestTriggerCollider.SetActive(false);
            }
            ///

        }
    }

    public void QuestCheck(string quest)
    {
        if(quest == "Grab a water bucket")
        {
            Destroy(_waypoint);
        }

    }

    public void QuestCompleteCheck(string quest)
    {

        //QuestPhotographForestTurtle
        if(quest =="Forest Turtle Adventure")
        {
            FireQuestTriggerCollider.SetActive(true);

            //waypoint
            _waypoint = (GameObject)Instantiate(Resources.Load("WaypointCanvas"));
            _waypoint.GetComponent<WaypointUI>().SetTarget(campWaypoint.transform);

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

        //QuestTalkDayTent
        if (quest == "Rest in the camp until morning")
        {
            characterLawrence.SetActive(false);
            characterLawrenceAftermath.SetActive(true);
        }

        //Quest
        //if(quest == "")

    }


}
