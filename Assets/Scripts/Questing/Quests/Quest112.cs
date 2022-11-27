using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest112 : QuestNew
{
    void Start()
    {

        questName = "A Second Beginning";
        questDescription = "Find 2 deer and Pick up 1 barrel ";
        reward = 10;
        questCompleted = false;

        StartCoroutine(IsQuestCompleted());

        UpdateQuestUI();

        //goal
        Goals.Add(new PictureGoal(this, "Deer", questDescription, false, 0, 2));
        Goals.Add(new CollectionGoal(this, "Barrel", questDescription, false, 0, 1));

        Debug.Log(Goals[0]);
        Debug.Log(Goals[1]);
        Goals.ForEach(g => g.InIt());
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
