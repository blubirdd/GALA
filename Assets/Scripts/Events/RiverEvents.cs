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
    public SubtleDialogueTrigger dialogueTurtleWaterPollution;

    [Header("River water")]
    public GameObject riverWaterObject;
    public Material cleanWater;
    public Material dirtyWater;

    [Header("Turtle oil")]
    public GameObject turtleOil;

    [Header("Cleaning river")]
    public GameObject[] oilBarrels;
    public GameObject fishingRod;

    [Header("Cutscene")]
    public GameObject cureTurtleCutscene;

    [Header("Characters")]
    public GameObject lawrenceRanger;
    public GameObject lawrenceAftermath;
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
            cureTurtleCutscene.SetActive(true);

            turtleOil.SetActive(true);
            //ffalse or destroy
            //fireQuest.SetActive(false);
            UIManager.instance.DisablePlayerMovement();

            StartCoroutine(DisableCharacterNPC());
            IEnumerator DisableCharacterNPC()
            {
                //cutscene duration
                dialogueTurtleWaterPollution.TriggerDialogue();

                yield return new WaitForSeconds(20f);
                //lawrenceAftermath.SetActive(false);

                UIManager.instance.EnablePlayerMovement();

            }
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


        //QuestCollectContaminatedBarrel
        if (questName == "Clean the river")
        {
            fishingRod.GetComponent<Outline>().enabled = true;
            //dialogueToGoToBeach.TriggerDialogue();
            foreach (var item in oilBarrels)
            {
                item.SetActive(true);
            }
        }

        //WARNING THIS USES A DIFFERENT EVENT!!!

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
        if (questName == "Clean the river")
        {
            dialogueToGoToBeach.TriggerDialogue();
            turtleOil.SetActive(false);
        }

        if (questName == "Help extinguish the fire")
        {
            Destroy(fireQuest);
        }


        //Quest Talk Day Tent
        if (questName == "Rest in the camp until morning")
        {
            riverWaterObject.GetComponent<Renderer>().material = dirtyWater;

            lawrenceAftermath.SetActive(true);
        }

    }

    private void OnDisable()
    {
        GameEvents.instance.onQuestCompleted -= RiverQuestCompleteCheck;
    }
}
