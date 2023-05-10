using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestFeedCrocodile : QuestNew
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
        ID = "QuestFeedCrocodile"; ;
        questName = "Learn about the crocodiles";
        questDescription = "Feed a Philippine Crocodile";

        goalDescription[0] = "Collect meat to feed the crocodiles";
        requiredAmount[0] = 1;

        goalDescription[1] = "Feed a Philippine Crocodile";
        requiredAmount[1] = 2;

        goalDescription[2] = "Photograph a Philippine Crocodile";
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
        Goals.Add(new CollectionGoal(this, "Meat", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.Add(new FeedGoal(this, "Philippine Crocodile", goalDescription[1], false, currentProgress[1], requiredAmount[1]));
        Goals.Add(new PictureGoal(this, "Philippine Crocodile", goalDescription[2], false, currentProgress[2], requiredAmount[2]));

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
        Inventory.instance.naturePoints += reward;
        //remove quest from task list
        Task.instance.RemoveTask(ID);

        //debug
        Debug.Log(this + " is Completed");

        //disable marker
        //Destroy(_waypoint);

        //wait for notifcation or cutscene
        yield return new WaitForSeconds(5f);
        //yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        AcceptQuest("QuestTalkKarlo1");
    }
}
