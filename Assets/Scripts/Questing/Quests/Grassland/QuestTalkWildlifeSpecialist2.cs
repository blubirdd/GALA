using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTalkWildlifeSpecialist2 : QuestNew
{
    static int numberOfGoals = 2;

    private string[] goalDescription = new string[numberOfGoals];
    private int[] currentProgress = new int[numberOfGoals];
    private int[] requiredAmount = new int[numberOfGoals];
    private string ID;

    private GameObject _waypoint;
    void Start()
    {

        //setup
        ID = "QuestTalkWildlifeSpecialist2"; ;
        questName = "Tamaraw Rescue";
        questDescription = "Report back to the wildlife specialist";

        goalDescription[0] = "Report back to the wildlife specialist";
        requiredAmount[0] = 1;

        goalDescription[1] = "Grab a medkit to save the Tamaraw";
        requiredAmount[1] = 1;

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
        Goals.Add(new TalkGoal(this, "Wildlife Specialist", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.Add(new CollectionGoal(this, "Medkit", goalDescription[1], false, currentProgress[1], requiredAmount[1]));
        Goals.ForEach(g => g.InIt());

        GetGoalsList();


        //waypoint
        SpawnWaypointMarker();

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
        _waypoint.GetComponent<WaypointUI>().SetTarget(WaypointManager.instance.waypointTransforms[7]);

    }

    IEnumerator IsQuestCompleted()
    {
        yield return new WaitUntil(() => questCompleted == true);

        //remove quest from task list
        Task.instance.RemoveTask(ID);

        //debug
        Debug.Log(this + " is Completed");

        //disable marker
        if(_waypoint != null)
        {
           Destroy(_waypoint);
        }


        yield return new WaitForSeconds(7f);
        AcceptQuest("QuestTalkWildlifeSpecialist3");
    }
}
