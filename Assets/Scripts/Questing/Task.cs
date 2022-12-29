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
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, TaskSave> keyValuePair in data.taskList)
        {
            AddTask(keyValuePair.Key, keyValuePair.Value.questTitle, keyValuePair.Value.goalDescription, keyValuePair.Value.progress, keyValuePair.Value.requiredAmount);
        }

        for(int i = 0; i < tasks.Count; i++)
        {
            quest = (QuestNew)quests.AddComponent(System.Type.GetType(tasks[i].ID));
        }
    }


    public void SaveData(GameData data)
    {
        data.taskList.Clear();

        for (int i = 0; i < tasks.Count; i++)
        {
            data.taskList.Add(tasks[i].ID, tasks[i]);
        }
    }
}
