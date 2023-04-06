using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInspectDugong : QuestNew
{
    static int numberOfGoals = 2;

    private string[] goalDescription = new string[numberOfGoals];
    private int[] currentProgress = new int[numberOfGoals];
    private int[] requiredAmount = new int[numberOfGoals];
    private string ID;

    public GameObject waypoint;
    void Start()
    {
        //setup
        ID = "QuestInspectDugong"; ;
        questName = "Approach the stranded Dugong";
        questDescription = "Approach the stranded Dugong";

        goalDescription[0] = "Inspect and approach the dugong";
        requiredAmount[0] = 1;

        goalDescription[1] = "Splash some water on the dugong";
        requiredAmount[1] = 3;

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



        //goal (this, name of target, goaldescription, iscompleted bool, current progress, required amount)
        Goals.Add(new CollectionGoal(this, "Stranded Dugong", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.Add(new TalkGoal(this, "Dugong", goalDescription[1], false, currentProgress[1], requiredAmount[1]));
        Goals.ForEach(g => g.InIt());
        UpdateQuestUI();
        GetGoalsList();

        //waypoint
        //SpawnWaypointMarker();

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

    public void SpawnWaypointMarker()
    {
        waypoint = (GameObject)Instantiate(Resources.Load("WaypointCanvas"));
        waypoint.GetComponent<WaypointUI>().SetTarget(WaypointManager.instance.waypointTransforms[4]);
    }

    IEnumerator IsQuestCompleted()
    {
        yield return new WaitUntil(() => questCompleted == true);

        //remove quest from task list
        Task.instance.RemoveTask(ID);

        //debug
        Debug.Log(this + " is Completed");

        //disable marker
        //Destroy(waypoint);

        //Add another quest
        yield return new WaitForSeconds(5f);
        AcceptQuest("QuestTalkStrandedDugongHelpers");
    }
}
