using System.Collections;
using UnityEngine;

public class QuestPhotoChicken : QuestNew
{
    private string[] goalDescription = new string[] { "Photograph a Red junglefowl" };
    private int[] currentProgress = new int[] { 0 };
    private int[] requiredAmount = new int[] { 1 };
    private string ID;

    public GameObject waypoint;
    void Start()
    {

        //setup
        ID = "QuestPhotoChicken"; ;
        questName = "Animals in Antrophogenic Biome";
        questDescription = "Photograph a Red junglefowl";


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
        Goals.Add(new PictureGoal(this, "Red junglefowl", goalDescription[0], false, currentProgress[0], requiredAmount[0]));
        Goals.ForEach(g => g.InIt());

        GetGoalsList();

        //event trigger
        //QuestEvents.instance.QuestAccepted2();

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
        waypoint.GetComponent<WaypointUI>().SetTarget(WaypointManager.instance.waypointTransforms[0]);
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
        //Destroy(waypoint);

        //Add another quest
        yield return new WaitForSeconds(5f);
        AcceptQuest("QuestTalkVillageChief2");
    }

}
