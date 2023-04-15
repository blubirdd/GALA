using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject injuredTurtle;
    public GameObject fireQuest;
    public GameObject[] fireTransformsToPutOut;
    [Header("Waypoint")]
    public GameObject smallWaypointPrefab;
    private GameObject[] waypointsForFire;

    [Header("Dialogues")]
    public SubtleDialogueTrigger dialogueToGoToBeach;

    [Header("River water")]
    public GameObject riverWaterObject;
    public Material cleanWater;
    public Material dirtyWater;
    void Start()
    {
        injuredTurtle.SetActive(false);

        GameEvents.instance.onQuestAcceptedForSave += RiverQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += RiverQuestCompleteCheck;

    }

    public void RiverQuestAcceptCheck(string questName)
    {
        //QuestUseMedkitTurtle
        if (questName == "Saving the Forest turtles")
        {
            injuredTurtle.SetActive(true);

            //ffalse or destroy
            //fireQuest.SetActive(false);
            Destroy(fireQuest);
        }

        //QuestPutOutFire
        if (questName == "Help extinguish the fire")
        {
            fireQuest.SetActive(true);
            //waypointsForFire = new GameObject[fireTransformsToPutOut.Length];
            //for (int i = 0; i < fireTransformsToPutOut.Length; i++)
            //{
            //    GameObject waypoint = Instantiate(smallWaypointPrefab, fireTransformsToPutOut[i].transform);
            //    waypoint.GetComponent<WaypointUI>().SetTarget(fireTransformsToPutOut[i].transform);
            //    waypointsForFire[i] = waypoint;
            //}
        }

    }
    public void RiverQuestCompleteCheck(string questName)
    {
        //QuestTalkDayTent
        //if(questName == "Rest in the camp until morning")
        //{
        //    injuredTurtle.SetActive(true);

        //    fireQuest.SetActive(false);
        //}

        //QuestCollectContaminatedBarrel
        if (questName == "Cleaning the river")
        {
            dialogueToGoToBeach.TriggerDialogue();
        }

        if (questName == "Help extinguish the fire")
        {
            Destroy(fireQuest);
        }


        //Quest Talk Day Tent
        if (questName == "Rest in the camp until morning")
        {
            riverWaterObject.GetComponent<Renderer>().material = dirtyWater;
        }

    }

    private void OnDisable()
    {
        GameEvents.instance.onQuestCompleted -= RiverQuestCompleteCheck;
    }
}
