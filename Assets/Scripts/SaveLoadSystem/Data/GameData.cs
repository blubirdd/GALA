using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;
using static Task;

[System.Serializable]
public class GameData
{

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
        this.naturePoints = 0;

        //to change
        playerPosition = Vector3.zero;

        itemsCollected = new SerializableDictionary<int, InventorySave>();
        taskList = new SerializableDictionary<string, TaskSave>();
    }
}
