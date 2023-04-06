using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasslandEvents : MonoBehaviour
{
    public GameObject wildTamaraws;
    public GameObject wildlifeSpecialist1;

    [Header("Wild animals")]
    public GameObject tamarawWildAnimalsParent;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.onQuestCompleted += GrasslandQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += GrasslandQuestCompleteCheck;
    }

    public void GrasslandQuestAcceptCheck(string questName)
    {
        //QuestUseMedkit2
        if(questName == "Curing the Tamaraws")
        {
            wildTamaraws.SetActive(true);
        }
    }
    public void GrasslandQuestCompleteCheck(string questName)
    {
        //QuestTalkWildlifeSpecialist2
        if(questName == "Report back to the wildlife specialist")
        {
            wildlifeSpecialist1.SetActive(false);
        }

        //QuestTalkWildlifeSpecialistFinal
        if(questName == "Report back to the specialist")
        {
            tamarawWildAnimalsParent.SetActive(true);
        }

        //QuestTalkRiverWildlifeBiologist
        if(questName == "Find out about this area")
        {
            tamarawWildAnimalsParent.SetActive(false);
        }

    }

    private void OnDisable()
    {
        GameEvents.instance.onQuestCompleted -= GrasslandQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted -= GrasslandQuestCompleteCheck;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
