using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasslandEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Task task;

    public GameObject wildTamaraws;
    public GameObject wildlifeSpecialist1;

    [SerializeField] private GameObject injuredTamaraw;
    [SerializeField] private GameObject curableTamaraw;
    [SerializeField] private GameObject wildlifeSpecialist2;

    [Header("Prefab to instantiate")]
    [SerializeField] private GameObject tamarawPrefab;

    [Header("Wild animals")]
    public GameObject tamarawWildAnimalsParent;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.onQuestCompleted += GrasslandQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += GrasslandQuestCompleteCheck;

        for (int i = 0; i < task.tasksCompeleted.Count; i++)
        {
            //scene setup
            //if (task.tasksCompeleted[i] == "QuestTalkWildlifeSpecialist2")
            //{
            //    Destroy(injuredTamaraw);
            //    curableTamaraw.SetActive(true);
            //}
        }

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
        //QuestCollectTamarawTracks
        if (questName == "Tracking the tamaraw")
        {
            injuredTamaraw.SetActive(true);
            curableTamaraw.SetActive(false);
            wildlifeSpecialist2.SetActive(false);
        }
        if (questName == "Tamaraw Rescue")
        {
            injuredTamaraw.SetActive(false);
            curableTamaraw.SetActive(true);
            wildlifeSpecialist2.SetActive(true);
        }

        if (questName == "Heal the Tamaraw")
        {
            Destroy(curableTamaraw);
            Instantiate(tamarawPrefab, curableTamaraw.transform.position, Quaternion.identity);
        }


        //QuestTalkWildlifeSpecialist2
        if (questName == "Report back to the wildlife specialist")
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
