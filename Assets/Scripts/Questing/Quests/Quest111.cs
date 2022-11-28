using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Quest111 : QuestNew
{


    void Start()
    {
        //event
        GameEvents.instance.onGoalValueChanged += GoalChanged;

        questName = "A new Beginning";
        questDescription = "Find 1 deer: ";
        reward = 10;
        questCompleted = false;

        StartCoroutine(IsQuestCompleted());

        UpdateQuestUI();

        //goal
        Goals.Add(new PictureGoal(this, "Deer", questDescription, false, 0, 1));
     
        Goals.ForEach(g => g.InIt());

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
        Debug.Log(this + " is Completed");
        

    }

   
}
