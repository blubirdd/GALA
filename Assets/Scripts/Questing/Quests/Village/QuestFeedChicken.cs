using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFeedChicken : QuestNew
{
    private string[] goalDescription = new string[] { "Throw and feed seeds to a Red junglefowl" };
    private int[] currentProgress = new int[] { 0 };
    private int[] requiredAmount = new int[] { 1 };
    private string ID;

    public GameObject waypoint;
    void Start()
    {

        //setup
        ID = "QuestFeedChicken"; ;
        questName = "Feed the Red junglefowls";
        questDescription = "Feed a Redjunglefowl";


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

        //goal (this, name of target, goaldescription, iscompleted bool, current progress, required amount)
        Goals.Add(new FeedGoal(this, "Red junglefowl", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.ForEach(g => g.InIt());

        GetGoalsList();

        //event trigger
        //QuestEvents.instance.QuestAccepted2();

        GameEvents.instance.QuestAcceptedForSave(questName);

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

        //disable marker
        Destroy(waypoint);

        //Add another quest
        yield return new WaitForSeconds(4f);
        AcceptQuest("QuestTalkVillageChief");
    }
}
