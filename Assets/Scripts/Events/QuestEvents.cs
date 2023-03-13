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
            }
        }
    }

    public void QuestCheck(string quest)
    {
        
    }

    public void QuestCompleteCheck(string quest)
    {

        if(quest == "QuestCollectTamarawTracks")
        {
            injuredTamaraw.SetActive(true);
        }

        if(quest == "QuestTalkWildlifeSpecialist2")
        {
            injuredTamaraw.SetActive(false);
            curableTamaraw.SetActive(true);
        }
    }


}
