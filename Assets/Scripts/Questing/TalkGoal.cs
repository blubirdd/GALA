using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TalkGoal : Goal
{
    public string npcName { get; set; }


    public TalkGoal(QuestNew quest, string npcName, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.quest = quest;
        this.npcName = npcName;
        this.description = description;
        this.goalCompleted = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;

    }

    public override void InIt()
    {
        base.InIt();
        TalkEvents.onCharacterApproach += CharacterApproached;

        //to evaluate saved data
        Debug.Log("Evaluating");
        Evaluate();

    }


    void CharacterApproached(ICharacter npc)
    {
        if (npc.npcName == this.npcName && quest.questCompleted == false)
        {
            this.currentAmount++;
            Evaluate();
        }
    }


}

