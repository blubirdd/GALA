using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedGoal : Goal
{
    public string animalName { get; set; }

    public FeedGoal(QuestNew quest,string animalName, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.quest = quest;
        this.animalName = animalName;
        this.description = description;
        this.goalCompleted = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;

    }

    public override void InIt()
    {
        base.InIt();
        FeedEvents.onAnimalFed += AnimalFed;

        //to evaluate saved data
        Debug.Log("Evaluating");
        Evaluate();

    }


    void AnimalFed(IAnimal animal)
    {   Debug.Log("AYO FEED FEED FEED FEED");
        if (animal.animalName == this.animalName && quest.questCompleted == false) 
        {
            this.currentAmount++;
            Evaluate();
        }    
    }

}
