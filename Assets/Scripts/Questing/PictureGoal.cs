using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureGoal : Goal
{
    public string animalName { get; set; }

    public PictureGoal(QuestNew quest,string animalName, string description, bool completed, int currentAmount, int requiredAmount)
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
        PictureEvents.onAnimalDiscovered += PictureTaken;
    }

    void PictureTaken(IAnimal animal)
    {
        if (animal.animalName == this.animalName && quest.questCompleted == false) 
        {
            this.currentAmount++;
            Evaluate();
            
        }    
    }


}
