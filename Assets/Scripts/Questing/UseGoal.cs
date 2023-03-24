using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseGoal : Goal
{
    public string itemName { get; set; }

    public UseGoal(QuestNew quest, string itemName, string description, bool completed, int currentAmount, int requiredAmount)
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

        Inventory.OnItemUsedCallback += UseItem_Goal;
        //to evaluate saved data
        Debug.Log("Evaluating");
        Evaluate();
    }

    void UseItem_Goal(Item item)
    {
        if (item.name == this.itemName && quest.questCompleted == false)
        {
            Debug.Log("QUEST - Detected use of item " + itemName);
            this.currentAmount++;
            Evaluate();

        }
    }
}
