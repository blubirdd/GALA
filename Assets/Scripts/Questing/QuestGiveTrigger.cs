using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiveTrigger : MonoBehaviour
{
    private QuestNew quest { get; set; }
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;

    public void OnTriggerEnter(Collider other)
    {
        if (questType != null)
        {
            //Assign Quest
            quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
        }
        this.gameObject.SetActive(false); 
    }
}
