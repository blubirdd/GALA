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
        questDescription = "Deer and Barrel";
        reward = 10;
        questCompleted = false;

        StartCoroutine(IsQuestCompleted());

        UpdateQuestUI();

        //goal
        Goals.Add(new PictureGoal(this, "Deer", "Take 2 pictures of Deer", false, 0, 2));
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
