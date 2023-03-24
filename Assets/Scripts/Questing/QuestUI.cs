using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class QuestUI : MonoBehaviour
{
    //singleton
    public static QuestUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of QuestUI found");
            return;
        }

        instance = this;
    }
    //

    public TextMeshProUGUI questNameTM;
    public TextMeshProUGUI questDescriptionTM;


    public void UpdateQuestName(string questName)
    {
        //questNameTM.SetText(questName);
    }

    public void UpdateQuestDescription(string questDescription)
    {
        //questDescriptionTM.SetText(questDescription);
    }

    public void ClearQuestTitle()
    {
        //questNameTM.SetText("No Quest Active");
    }

    public void ClearQuestDescription()
    {
        //questDescriptionTM.SetText("Explore the area");
    }
}
