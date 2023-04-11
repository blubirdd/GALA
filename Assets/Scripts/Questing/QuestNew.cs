using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


[System.Serializable]
public class QuestNew : MonoBehaviour
{


    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string questID { get; set; }
    public string questName { get; set; }
    public string questDescription { get; set; }
    public int reward { get; set; }
    public bool questCompleted { get; set; }

    public string _questName;
    public string _questDescription;
    public bool _questCompleted;

    //public void SaveData(GameData data)
    //{

    //}

    private void Start()
    {
        GameEvents.instance.onQuestCompleted += QuestComplete;


    }

    public void Initialize()
    {
        _questName = questName;
        _questDescription = questDescription;
        _questCompleted = questCompleted;

        foreach (Goal goal in Goals)
        {
            Debug.Log(goal.description + " progress " + goal.currentAmount);
        }

        QuestTaskUI.instance.UpdateQuestUI();
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
            //Initialize();
            StartCoroutine(CompleteQuest());
            IEnumerator CompleteQuest()
            {
                //yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);

                //update quest UI
                Debug.Log("Quest is Completed!!!");

                //QuestUI.instance.ClearQuestTitle();
                //QuestUI.instance.ClearQuestDescription();

                GameEvents.instance.QuestCompleted(questName);
                QuestTaskUI.instance.UpdateQuestUI();

                yield return null;
            }

        }

    }

    public void AcceptQuest(string questName)
    {
        this.gameObject.AddComponent(System.Type.GetType(questName));
    }

    public void RemoveQuest(string questName)
    {
        Component componentToRemove = this.gameObject.GetComponent(System.Type.GetType(questName));
        if (componentToRemove != null)
        {
            Destroy(componentToRemove);
        }
    }
}
