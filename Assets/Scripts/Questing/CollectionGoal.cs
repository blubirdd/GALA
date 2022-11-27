using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGoal : Goal
{
    public string itemName { get; set; }

 

    void Start()
    {
 

    }
    public CollectionGoal(QuestNew quest,string itemName, string description, bool completed, int currentAmount, int requiredAmount)
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

        //  PictureEvents.onAnimalDiscovered += PictureTaken;
        Inventory.OnItemAddedCallback += ItemPickedUp;
    }

    void ItemPickedUp(Item item)
    {
        if (item.name == this.itemName && quest.questCompleted == false) 
        {
            Debug.Log("Detected item " + itemName) ;
            this.currentAmount++;
            Evaluate();
            
        }  
    }


}
