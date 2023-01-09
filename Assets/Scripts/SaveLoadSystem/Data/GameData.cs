using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;
using static Task;

[System.Serializable]
public class GameData
{

    public bool triggerStartDialogue;
    public Vector3 playerPosition;
    public int naturePoints;

    //inventory
    public SerializableDictionary<int, InventorySave> itemsCollected;

    // tasks or objectives
    public SerializableDictionary<string, TaskSave> taskList;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        //start game
        triggerStartDialogue = true;

        this.naturePoints = 0;


        //to change
        playerPosition = new Vector3(-307.29f, 2, 353.81f);
        //playerPosition = Vector3.zero;


        itemsCollected = new SerializableDictionary<int, InventorySave>();
        taskList = new SerializableDictionary<string, TaskSave>();
    }
}
