using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RiverEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject injuredTurtle;
    public GameObject fireQuest;
    public GameObject[] fireTransformsToPutOut;
    public GameObject[] fireSmokeToDisable;
    [Header("Waypoint")]
    public GameObject smallWaypointPrefab;
    private GameObject[] waypointsForFire;

    [Header("Dialogues")]
    public SubtleDialogueTrigger dialogueToGoToBeach;
    public SubtleDialogueTrigger dialogueTurtleWaterPollution;

    //[Header("Fire Quest")]
    //public GameObject questStatusHintCanvas;
    //public TextMeshProUGUI questStatusText;
    [Header("River water")]
    public GameObject riverWaterObject;
    //public Material cleanWater;
    //public Material dirtyWater;

    public Material riverMaterial;
    private Color originalColor;

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

    [Header("GiveItem")]
    public Item flashLightTogive;
    public GameObject waterBucket;

    [Header("River Border To Swamp")]
    public GameObject riverGuard;
    public GameObject swampBridgeWall;
    public GameObject swampLocation;

    void Start()
    {
       originalColor = riverMaterial.GetColor("_Color");
       riverMaterial.SetFloat("_Opacity", 0.6f);
        injuredTurtle.SetActive(false);

        GameEvents.instance.onQuestAcceptedForSave += RiverQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += RiverQuestCompleteCheck;

        if (Task.instance.tasksCompeleted.Contains("QuestTakeRiverQuiz"))
        {
            swampBridgeWall.SetActive(false);
        }

    }

    public void RiverQuestAcceptCheck(string questName)
    {
        //QuestUseMedkitTurtle
        if (questName == "Saving the Forest turtles")
        {
            cureTurtleCutscene.SetActive(false);
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
            //Destroy(fireQuest);
        }

        if(questName == "Grab a water bucket")
        {
            waterBucket.SetActive(true);
        }

        //QuestPutOutFire
        if (questName == "Help extinguish the fire")
        {
            fireQuest.SetActive(true);


            foreach (var item in fireTransformsToPutOut)
            {
                item.SetActive(true);
            }

            foreach (var item in fireSmokeToDisable)
            {
                item.SetActive(false);
            }

            //enable queststatushint
            //questStatusHintCanvas.SetActive(true);
            //questStatusText.text = "Put out the fire before the timer runs out!";

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
            turtleOil.SetActive(false);
            injuredTurtle.SetActive(false);
        }

        //QuestTalkNightTent
        if(questName == "Wait until night time.")
        {
            StartCoroutine(WaitForDialogue());
            IEnumerator WaitForDialogue()
            {
                yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
                Inventory.instance.Add(flashLightTogive, 1, true);
            }
        }

        //QuestTalkRiverGuard
        if (questName == "Try to cross the river")
        {
            riverGuard.SetActive(true);
        }

        //QuestTalkDayTent
        if (questName == "Rest in the camp until morning")
        {
            fireQuest.SetActive(false);
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
        if (questName == "Saving the Forest turtles")
        {
            injuredTurtle.SetActive(false);
        }


            //QuestCollectContaminatedBarrel
        if (questName == "Clean the river")
        {
            dialogueToGoToBeach.TriggerDialogue();
            turtleOil.SetActive(false);

            //clean water
            riverMaterial.SetColor("_Color", originalColor);
            riverMaterial.SetFloat("_Opacity", 0.6f);
        }

        //QuestPutOutFire
        if (questName == "Help extinguish the fire")
        {
            Debug.Log("TURN OFF QUEST STATUS");
            fireQuest.SetActive(false);
            //questStatusHintCanvas.SetActive(false);
            //Destroy(fireQuest);
        }


        //Quest Talk Day Tent
        if (questName == "Rest in the camp until morning")
        {
            //riverWaterObject.GetComponent<Renderer>().material = dirtyWater;
            riverMaterial.SetColor("_Color", Color.black);
            riverMaterial.SetFloat("_Opacity", 1.0f);
            lawrenceAftermath.SetActive(true);
        }


        if(questName == "Try to cross the bridge")
        {
            swampBridgeWall.SetActive(false);
            swampLocation.SetActive(true);
        }
    }



    //private void OnDisable()
    //{
    //    GameEvents.instance.onQuestCompleted -= RiverQuestCompleteCheck;
    //}
}
