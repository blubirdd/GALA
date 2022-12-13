using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest112 : QuestNew
{
    void Start()
    {
        //event
        GameEvents.instance.onGoalValueChanged += GoalChanged;

        questName = "A Second Beginning";
        questDescription = "Tap for more info";
        reward = 10;
        questCompleted = false;

        StartCoroutine(IsQuestCompleted());

        UpdateQuestUI();

        //goal
        Goals.Add(new PictureGoal(this, "Philippine Eagle", "Take a picture of Philippine Eagle", false, 0, 1));
        Goals.Add(new PictureGoal(this, "Tamaraw", "Take a picture of Tamaraw", false, 0, 1));
        Goals.Add(new CollectionGoal(this, "Barrel", "Collect 1 Barrel", false, 0, 1));

        Goals.ForEach(g => g.InIt());

        //event trigger
        GetGoalsList();
    }

    private void GetGoalsList()
    {
        GameEvents.QuestAccepted(Goals);

    }

    public void GoalChanged()
    {
        GetGoalsList();
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
        Debug.Log(this+ " is Completed");


    }
}
