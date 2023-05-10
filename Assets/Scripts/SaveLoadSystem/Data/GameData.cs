using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Book;
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

    //Photos
    public SerializableDictionary<int, PhotosSave> photosCollected;
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

    public bool hasBackpack;

    //TIME
    public float time;


    //QUIZZES
    public int villageQuizScore;
    public int grasslandQuizScore;
    public int riverQuizScore;
    public int swampQuizScore;
    public int rainForestQuizScore;


    //AUDIO
    public float musicVolume;
    public float SFXVolume;

    //THREATS
    public List<string> threatsDiscovered;

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

        //inventory
        itemsCollected = new SerializableDictionary<int, InventorySave>();

        //photos
        photosCollected = new SerializableDictionary<int, PhotosSave>();

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

        hasBackpack = false;

        playerLocation = "Village";

        time = 10;

        villageQuizScore = 0;
        grasslandQuizScore = 0;
        riverQuizScore = 0;
        swampQuizScore = 0;
        rainForestQuizScore = 0;


        //AUDIO
        musicVolume = 0.8f;
        SFXVolume = 1f;

        //threats
        threatsDiscovered = new List<string>();
    }



}

//public class LevelOneDAta
//{
//    public List<string> tasksCompleted;
//}

