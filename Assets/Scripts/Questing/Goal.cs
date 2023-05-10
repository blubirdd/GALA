using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Goal
{
    public QuestNew quest { get; set; }
    public string description { get; set; }
    public bool goalCompleted { get; set; }
    public int currentAmount { get; set; }
    public int requiredAmount { get; set; }

    public virtual void InIt()
    {
        
    }


    public void Evaluate()
    {
        //event - Goal value changed
        QuestTaskUI.instance.UpdateQuestUI();
        GameEvents.instance.GoalValueChanged();

        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
 
    }

    public void Complete()
    {
        Debug.Log("Goal Completed");

        SoundManager.instance.PlaySoundFromClips(11);
        goalCompleted = true; //on top if single goal only

        quest.CheckGoals();

        
    }

}
