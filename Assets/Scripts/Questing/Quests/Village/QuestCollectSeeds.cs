using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollectSeeds : QuestNew
{
    private string[] goalDescription = new string[] { "Grab 3 bag of seeds from the nearby house" };
    private int[] currentProgress = new int[] { 0 };
    private int[] requiredAmount = new int[] { 3 };
    private string ID;

    public GameObject waypoint;
    void Start()
    {

        //setup
        ID = "QuestCollectSeeds"; ;
        questName = "Help the village farmer";
        questDescription = "Grab seeds from the nearby house";


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
        Goals.Add(new CollectionGoal(this, "Seeds", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.ForEach(g => g.InIt());

        GetGoalsList();

        //event trigger
        //QuestEvents.instance.QuestAccepted2();

        //waypoint
        SpawnWaypointMarker();

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

    public void SpawnWaypointMarker()
    {
        Transform waypointParent = FindObjectOfType<WaypointParent>(true).gameObject.transform;
        waypoint = (GameObject)Instantiate(Resources.Load("WaypointCanvas"), waypointParent);
        waypoint.GetComponent<WaypointUI>().SetTarget(WaypointManager.instance.waypointTransforms[2]);
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
        yield return new WaitForSeconds(2f);
        AcceptQuest("QuestTalkFarmer");
    }
}
