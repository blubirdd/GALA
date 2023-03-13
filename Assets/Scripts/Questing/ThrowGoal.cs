using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGoal : Goal
{
    public string itemName {get; set;}
    
    public ThrowGoal (QuestNew quest, string itemName, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.quest = quest;
        this.itemName = itemName;
        this.description = description;
        this.goalCompleted = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    public override void InIt()
    {
        base.InIt();
        ThrowEvents.onItemThrown += ItemThrown;

        //to evaluate saved data
        Debug.Log("Evaluating");
        Evaluate();

    }

    void ItemThrown(IThrowable item)
    {
        if(item.itemName == this.itemName && quest.questCompleted == false)
        {
            this.currentAmount++;
            Evaluate();
        }
    }
}
