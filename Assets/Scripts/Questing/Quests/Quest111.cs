using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Quest111 : QuestNew
{


    void Start()
    {
        //event
    

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
        GameEvents.QuestAccepted(Goals);


    }

    private void GetGoalsList()
    {
      //  UIManager.GetObjectives(Goals);

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
