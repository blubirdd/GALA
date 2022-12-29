using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]
public class Quest111 : QuestNew 
{

    static int numberOfGoals = 2;

    private string[] goalDescription = new string[numberOfGoals];
    private int[] currentProgress = new int[numberOfGoals];
    private int[] requiredAmount = new int[numberOfGoals];
    private string ID;

    void Start()
    {


        //setup
        ID = "Quest111";
        questName = "A new Beginning";
        questDescription = "deer and tamaraw";

        goalDescription[0] = "Find 2 deers";
        goalDescription[1] = "Find 2 tamaraws";

        requiredAmount[0] = 2;
        requiredAmount[1] = 2;

        reward = 10;
        questCompleted = false;

        //pass the progress from task class to here
        for (int i = 0; i < Task.instance.tasks.Count; i++)
        {
            if(Task.instance.tasks[i].ID == ID)
            {
                currentProgress = Task.instance.tasks[i].progress;
            }

        }

        //add to task list
        Task.instance.AddTask(ID, questName, goalDescription,currentProgress, requiredAmount);


        //event
        GameEvents.instance.onGoalValueChanged += GoalChanged;

        //start coroutine
        StartCoroutine(IsQuestCompleted());

        UpdateQuestUI();

        //goal
        Goals.Add(new PictureGoal(this, "Visayan Spotted Deer", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.Add(new PictureGoal(this, "Tamaraw", goalDescription[1], false, currentProgress[1], requiredAmount[1]));
        Goals.ForEach(g => g.InIt());

        GetGoalsList();

        //event trigger
        //QuestEvents.instance.QuestAccepted2();

    }

    private void GetGoalsList()
    {
        GameEvents.QuestAccepted(ID, Goals);
    }

    public void GoalChanged()
    {
        GetGoalsList();

        for (int i = 0; i < Goals.Count; i++)
        {
            currentProgress[i] = Goals[i].currentAmount;
        }

        SendProgress();
    }

    public void SendProgress()
    {
        Task.instance.UpdateProgress(ID, currentProgress);
    }
    public void UpdateQuestUI()
    {
        QuestUI.instance.UpdateQuestName(questName);

        QuestUI.instance.UpdateQuestDescription(questDescription);

        Initialize();
    }

   IEnumerator IsQuestCompleted()
    {
        yield return new WaitUntil(() => questCompleted == true);

        //remove quest from task list
        Task.instance.RemoveTask(ID);

        //debug
        Debug.Log(this + " is Completed");

    }

}
