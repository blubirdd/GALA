using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestTalkVillageChief : QuestNew
{
    private string[] goalDescription = new string[] { "Talk to the Village Chief" };
    private int[] currentProgress = new int[] { 0 };
    private int[] requiredAmount = new int[] { 1 };
    private string ID;

    private GameObject _waypoint;
    void Start()
    {
     
        //setup
        ID = "QuestTalkVillageChief"; ;
        questName = "The AlGames Village";
        questDescription = "Talk to the Village Chief";


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
        Goals.Add(new TalkGoal(this, "Village Chief", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.ForEach(g => g.InIt());

        GetGoalsList();

        //event trigger
        //QuestEvents.instance.QuestAccepted2();

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
       _waypoint.GetComponent<WaypointUI>().SetTarget(WaypointManager.instance.waypointTransforms[0]);
       // waypoint.name = WaypointManager.instance.waypointTransforms[0].name + "Waypoint";
    }

    IEnumerator IsQuestCompleted()
    {
        yield return new WaitUntil(() => questCompleted == true);

        //remove quest from task list
        Task.instance.RemoveTask(ID);

        //debug
        Debug.Log(this + " is Completed");

        //disable marker
        //Destroy(GameObject.Find("Village ChiefWaypoint").gameObject);
        Destroy(_waypoint);
    }
}
