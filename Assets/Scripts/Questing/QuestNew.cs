using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class QuestNew: MonoBehaviour
{


    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string questName { get; set; }
    public string questDescription { get; set; }
    public int reward { get; set; }
    public bool questCompleted { get; set; }

    [SerializeField] private string _questName;
    [SerializeField] private string _questDescription;
    [SerializeField] private bool _questCompleted;


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
            QuestComplete();
        }
    }
    
    public void QuestComplete()
    {
        if (questCompleted == true)
        {
            Initialize();

            //update quest UI
            Debug.Log("Quest is Completed!!!");

            QuestUI.instance.ClearQuestTitle();
            QuestUI.instance.ClearQuestDescription();

            GameEvents.instance.QuestCompleted();
        }

    }

    void ResetQuest()
    {
        
    }

    void GiveReward()
    {
        
    }



}
