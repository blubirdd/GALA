using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


[System.Serializable]
public class QuestNew: MonoBehaviour
{


    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string questName { get; set; }
    public string questDescription { get; set; }
    public int reward { get; set; }
    public bool questCompleted { get; set; }

    public string _questName;
    public string _questDescription;
    public bool _questCompleted;


    private void Start()
    {
        GameEvents.instance.onQuestCompleted += QuestComplete;
    }

    public void Initialize()
    {
        _questName = questName;
        _questDescription = questDescription;
        _questCompleted = questCompleted;
    }
    public void CheckGoals()
    {
        if (Goals.All(g => g.goalCompleted))
        {
            questCompleted = true;
            QuestComplete(this.questName);
        }

    }
    
    public void QuestComplete(string questName)
    {
        if (questCompleted == true)
        {
            Initialize();

            //update quest UI
            Debug.Log("Quest is Completed!!!");

            QuestUI.instance.ClearQuestTitle();
            QuestUI.instance.ClearQuestDescription();

            GameEvents.instance.QuestCompleted(this.questName);
        }

    }

}
