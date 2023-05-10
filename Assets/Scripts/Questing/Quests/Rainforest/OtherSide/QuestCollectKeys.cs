using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollectKeys : QuestNew
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
        ID = "QuestCollectKeys"; ;
        questName = "Grab keys for the animal cages";
        questDescription = "Grab keys for the cages";

        goalDescription[0] = "Grab the bird cage key";
        requiredAmount[0] = 1;

        goalDescription[1] = "Grab the small cage key";
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
        Goals.Add(new CollectionGoal(this, "Bird Cage Key", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.Add(new CollectionGoal(this, "Small Cage Key", goalDescription[1], false, currentProgress[1], requiredAmount[1]));

        Goals.ForEach(g => g.InIt());

        GetGoalsList();


        //waypoint
        //SpawnWaypointMarker();
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
        _waypoint = (GameObject)Instantiate(Resources.Load("WaypointCanvas"), waypointParent);
        _waypoint.GetComponent<WaypointUI>().SetTarget(WaypointManager.instance.waypointTransforms[3]);

    }

    IEnumerator IsQuestCompleted()
    {
        yield return new WaitUntil(() => questCompleted == true);

        //remove quest from task list
        Task.instance.RemoveTask(ID);

        //debug
        Debug.Log(this + " is Completed");

        //disable marker
        //Destroy(_waypoint);

        yield return new WaitForSeconds(5f);
        AcceptQuest("QuestSaveAnimals");
    }
}