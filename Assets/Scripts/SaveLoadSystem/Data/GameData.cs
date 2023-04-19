using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;
using static Task;

[System.Serializable]
public class GameData
{
    public int gameLevel; 

    public bool triggerStartDialogue;
    public Vector3 playerPosition;
    public int naturePoints;

    //play name
    public string playerName;
    //player location
    public string playerLocation;

    //inventory
    public SerializableDictionary<int, InventorySave> itemsCollected;

    // tasks or objectives
    public List<string> tasksCompeleted;
    public SerializableDictionary<string, TaskSave> taskList;

    //NPC's talked to
    public SerializableDictionary<string, bool> NPCsTalked;
    public SerializableDictionary<string, bool> NPCsCompleted;

    //CUTSCENES TRIGGERED
    public SerializableDictionary<string, bool> cutsceneTriggered;

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

        tasksCompeleted = new List<string>();
        taskList = new SerializableDictionary<string, TaskSave>();

        //NPCs
        NPCsTalked = new SerializableDictionary<string, bool>();
        NPCsCompleted = new SerializableDictionary<string, bool>();

        //cutscene
        cutsceneTriggered = new SerializableDictionary<string, bool>();

        gameLevel = 1;

        playerLocation = "Village";

    }



}

public class LevelOneDAta
{
    public List<string> tasksCompleted;
}

