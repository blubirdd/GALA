using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class Task : MonoBehaviour, IDataPersistence
{
    #region Singleton

    public static Task instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of task found");
            return;
        }

        instance = this;
    }
    #endregion

    [SerializeField] private GameObject quests;
    public List<string> tasksCompeleted = new List<string>();

    [NonReorderable] public List<TaskSave> tasks = new List<TaskSave>();
    private QuestNew quest { get; set; }


    public void AddTask(string id, string questTitle, string[] goalDescription, int[] progress, int[] requiredAmount)
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].ID == id)
            {
                return;
            }
        }

        tasks.Add(new TaskSave(id, questTitle, goalDescription, progress, requiredAmount));

        GameEvents.instance.QuestAcceptedNotification(questTitle);
    }

    

    [System.Serializable]
    public class TaskSave
    {
        public string ID;
        public string questTitle;
        public string[] goalDescription;
        public int[] progress;
        public int[] requiredAmount;

 
        
        public TaskSave(string id, string questTitle, string[] goalDescription ,int[] progress, int[] requiredAmount)
        {
            ID = id;
            this.questTitle = questTitle;
            this.goalDescription = goalDescription;
            this.progress = progress;
            this.requiredAmount = requiredAmount;


        }
    }

    public void UpdateProgress(string id, int[] currentProgress)
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].ID == id)
            {
                tasks[i].progress = currentProgress;
            }
        }
    }



    public void RemoveTask(string id)
    {
        tasks.RemoveAll(tasks=> id == tasks.ID);
        tasksCompeleted.Add(id);
        
    }

    public void RemoveTaskNotComplete(string id)
    {
        tasks.RemoveAll(tasks => id == tasks.ID);
    }

    public void LoadData(GameData data)
    {
        //load the current quests in json

        foreach (KeyValuePair<string, TaskSave> keyValuePair in data.taskList)
        {
            AddTask(keyValuePair.Key, keyValuePair.Value.questTitle, keyValuePair.Value.goalDescription, keyValuePair.Value.progress, keyValuePair.Value.requiredAmount);
        }

        //accept the existing quests in load data
        for (int i = 0; i < tasks.Count; i++)
        {
            quest = (QuestNew)quests.AddComponent(System.Type.GetType(tasks[i].ID));
        }

        //load the tasks completed
        for (int i = 0; i < data.tasksCompeleted.Count; i++)
        {
            tasksCompeleted.Add(data.tasksCompeleted[i]);
        }



        //foreach(Goal goal in QuestNew)
    }


    public void SaveData(GameData data)
    {
        data.taskList.Clear();
        data.tasksCompeleted.Clear();
        //add list of quest
        for (int i = 0; i < tasks.Count; i++)
        {
            data.taskList.Add(tasks[i].ID, tasks[i]);
        }

        //add list of completed quests
        for (int i = 0; i < tasksCompeleted.Count; i++)
        {
            data.tasksCompeleted.Add(tasksCompeleted[i]);
        }

    }
}
