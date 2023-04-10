using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quest11 : MonoBehaviour
{
    public Quest quest;

    public Player player;

    public GameObject questUI;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;

    public Animator animator;

    public void AcceptQuest()
    {
        
        //quest.isActive = true;
        //UpdateQuestUI();
        //player.quest = quest;
        //animator.SetBool("isOpen", true);
    }

    public void UpdateQuestUI()
    {
        if (quest.isActive == true)
        {
            questTitle.SetText(quest.title);
            questDescription.SetText(quest.description);
        }

        else
        {
            questTitle.SetText("No Quest Active");
            questDescription.SetText("test");
        }

    }

}
