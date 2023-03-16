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

    [Header("Grasslands Tamaraw Prefab")]
    public GameObject tamarawPrefab;

    private void Start()
    {
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



    }


}
