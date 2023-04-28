using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;
using static Task;

[System.Serializable]
public class GameData
{
    //public int gameLevel; 

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

    //2D minigames
    public int eggGameScore;
    public int moleGameScore;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        //start game
        triggerStartDialogue = true;

        this.naturePoints = 0;


        //to change
        playerPosition = new Vector3(-307.29f, 2, 353.81f);

        //temporary for demo
        //playerPosition = new Vector3(188.4223f, 1, 85.26123f);
        //playerPosition = Vector3.zero;


        itemsCollected = new SerializableDictionary<int, InventorySave>();

        tasksCompeleted = new List<string>();
        taskList = new SerializableDictionary<string, TaskSave>();

        //NPCs
        NPCsTalked = new SerializableDictionary<string, bool>();
        NPCsCompleted = new SerializableDictionary<string, bool>();

        //cutscene
        cutsceneTriggered = new SerializableDictionary<string, bool>();

        //2D Minigame
        eggGameScore = 0;
        moleGameScore = 0;
        //gameLevel = 1;

        playerLocation = "Village";

    }



}

//public class LevelOneDAta
//{
//    public List<string> tasksCompleted;
//}

