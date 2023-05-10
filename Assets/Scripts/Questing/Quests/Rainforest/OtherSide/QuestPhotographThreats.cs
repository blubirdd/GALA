using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPhotographThreats : QuestNew
{
    static int numberOfGoals = 3;

    private string[] goalDescription = new string[numberOfGoals];
    private int[] currentProgress = new int[numberOfGoals];
    private int[] requiredAmount = new int[numberOfGoals];
    private string ID;

    private GameObject _waypoint;
    void Start()
    {

        //setup
        ID = "QuestPhotographThreats"; ;
        questName = "Photograph and gather evidence";
        questDescription = "Gather evidence of the destructon of habitat";

        goalDescription[0] = "Photograph the mines";
        requiredAmount[0] = 2;

        goalDescription[1] = "Photograph deforested trees";
        requiredAmount[1] = 2;

        goalDescription[2] = "Free caged animal";
        requiredAmount[2] = 1;
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
        Goals.Add(new PictureGoal(this, "Mining Threat", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.Add(new PictureGoal(this, "Deforestation Threat", goalDescription[1], false, currentProgress[1], requiredAmount[1]));
        Goals.Add(new TalkGoal(this, "Hornbill", goalDescription[2], false, currentProgress[2], requiredAmount[2]));

        Goals.ForEach(g => g.InIt());

        GetGoalsList();


        //waypoint
        //SpawnWaypointMarker();
        GameEvents.instance.QuestAcceptedForSave(questName);

        IndicatorController.instance.EnableCameraIndicator();

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
        _waypoint = (GameObject)Instantiate(Resources.Load("WaypointCanvas"));
        _waypoint.GetComponent<WaypointUI>().SetTarget(WaypointManager.instance.waypointTransforms[3]);

    }

    IEnumerator IsQuestCompleted()
    {
        yield return new WaitUntil(() => questCompleted == true);
        Inventory.instance.naturePoints += reward;
        //remove quest from task list
        Task.instance.RemoveTask(ID);

        //debug
        Debug.Log(this + " is Completed");

        //disable marker
        //Destroy(_waypoint);

        //wait for notifcation or cutscene
        //yield return new WaitForSeconds(5f);
        //AcceptQuest("QuestTalkLawrence");
    }
}
