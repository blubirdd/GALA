using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollectionTest : QuestNew
{
    void Start()
    {

        questName = "Collection item test check";
        questDescription = "Find 1 barrel: ";
        reward = 10;
        questCompleted = false;

        StartCoroutine(IsQuestCompleted());

        UpdateQuestUI();

        //goal
        Goals.Add(new PictureGoal(this, "Deer", questDescription, false, 0, 1));

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
        Debug.Log(this + " is Completed");


    }
}
