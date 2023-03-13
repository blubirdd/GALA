using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

[System.Serializable]
public class QuestIntroPart2 : QuestNew
{
    static int numberOfGoals = 4;

    private string[] goalDescription = new string[numberOfGoals];
    private int[] currentProgress = new int[numberOfGoals];
    private int[] requiredAmount = new int[numberOfGoals];
    private string ID;

    void Start()
    {


        //setup
        ID = "QuestIntroPart2";
        questName = "Preparation for Adventure";
        questDescription = "Grab items inside the house\nand open the door";

        goalDescription[0] = "Grab the camera";
        requiredAmount[0] = 1;

        goalDescription[1] = "Grab the journal";
        requiredAmount[1] = 1;

        goalDescription[2] = "Grab your backpack";
        requiredAmount[2] = 1;

        goalDescription[3] = "Grab keys to the door";
        requiredAmount[3] = 1;

        reward = 10;

        questCompleted = false;

        //pass the progress from task class to here
        for (int i = 0; i < Task.instance.tasks.Count; i++)
        {
            if (Task.instance.tasks[i].ID == ID)
            {
                currentProgress = Task.instance.tasks[i].progress;
            }

        }

        //add to task list
        Task.instance.AddTask(ID, questName, goalDescription, currentProgress, requiredAmount);


        //event
        GameEvents.instance.onGoalValueChanged += GoalChanged;

        //start coroutine
        StartCoroutine(IsQuestCompleted());

        UpdateQuestUI();

        //goals
        Goals.Add(new CollectionGoal(this, "Camera", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.Add(new CollectionGoal(this, "Book", goalDescription[1], false, currentProgress[1], requiredAmount[1]));
        Goals.Add(new CollectionGoal(this, "Backpack", goalDescription[2], false, currentProgress[2], requiredAmount[2]));
        Goals.Add(new CollectionGoal(this, "Key", goalDescription[3], false, currentProgress[3], requiredAmount[3]));

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
