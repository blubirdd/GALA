using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachEvents : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject strandedDugong;
    public GameObject dugongHelper1;
    public GameObject dugongHelper2;
    public GameObject questParent;

    //[Header("Triggers")]
    [Header("First Set Reference")]
    public GameObject firstCharacterSet;


    [Header("Quest 2")]

    public GameObject secondStrandedDuong;
    public GameObject secondCharacterSet;

    public GameObject pushDugongTimeline;
    [Header("Photo")]
    public Photograph dugongPhotograph;

    [Header("Quest 3")]
    public GameObject thirdCharacterSet;
    public GameObject monCharacter3;
    public GameObject dolphinLoop1;
    public GameObject dolphinLoop2;
    public GameObject npcWalkingTimeline;

    public GameObject playerRepositionPoint;

    [Header("Beach trash")]
    public GameObject beachTrashToClean;
    public GameObject grabWaterQuest;

    [Header("Dialogues")]
    public SubtleDialogueTrigger dolphinEntranceDialogue;
    [Header("Timeline")]
    public GameObject dolphinEntranceTimeline;

    [Header("Stick item")]
    public Item stick;
    public Item bucket;

    void Start()
    {
        secondCharacterSet.SetActive(false);
        GameEvents.instance.onQuestCompleted += BeachQuestCompleteCheck;
        GameEvents.instance.onQuestAcceptedForSave += BeachQuestAcceptCheck;

    }

    public void BeachQuestAcceptCheck(string questName)
    {
        //QuestInspectDugong
        if(questName == "Approach the stranded Dugong")
        {
            strandedDugong.SetActive(true);
            Inventory.instance.Add(bucket, 1 , false);
            //strandedDugong.GetComponent<Outline>().enabled = true;
        }


        //QuestPushDugong
        //if (questName == "Push the stranded Dugong")
        //{
        //    //strandedDugong.GetComponent<Outline>().enabled = true;
        //}

        //QuestCleanBeachTrash
        if (questName == "Clean the beach")
        {
            Book.instance.AddAnimalPhoto(dugongPhotograph);
            Inventory.instance.itemDiscovery.NewItemDiscovered(dugongPhotograph.polaroidPhoto, dugongPhotograph.name, "New Animal Discovered. Check your journal for more details", false);

            beachTrashToClean.SetActive(true);
            npcWalkingTimeline.SetActive(true);
            grabWaterQuest.SetActive(false);
            secondCharacterSet.SetActive(false);
            UIManager.instance.EnablePlayerMovement();
        }

        //QuestTalkStrandedDugongHelpers
        if (questName == "Find help to save the Dugong")
        {
            firstCharacterSet.SetActive(true);
        }

        //QuestPushDugong
        if(questName == "Push the stranded Dugong")
        {
            
        }


    }

    public void BeachQuestCompleteCheck(string questName)
    {
        //QuestTalkStrandedDugongHelpers
        //if(questName == "Find help to save the Dugong")
        //{
        //    dugongHelper1.GetComponent<Animator>().SetBool("Idle", true);
        //    dugongHelper2.GetComponent<Animator>().SetBool("Idle", true);
        //}

        //QuestPushDugong
        if(questName == "Push the stranded Dugong")
        {
            pushDugongTimeline.SetActive(true);
            UIManager.instance.DisablePlayerMovement();
            //strandedDugong.GetComponent<Outline>().enabled = false;
            strandedDugong.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;
            //strandedDugong.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            StartCoroutine(PushDugong());
            IEnumerator PushDugong()
            {
                float distanceMoved = 0f;
                Vector3 startPosition = questParent.transform.position;
                Vector3 endPosition = questParent.transform.position + questParent.transform.forward * 13f - Vector3.up * 1f;

                while (distanceMoved < 13f)
                {
                    float distanceThisFrame = 3f * Time.deltaTime;
                    questParent.transform.position = Vector3.MoveTowards(questParent.transform.position, endPosition, distanceThisFrame);
                    distanceMoved += distanceThisFrame;
                    yield return null;
                }

                yield return new WaitForSeconds(2f);
                dugongHelper1.GetComponent<Animator>().SetBool("Idle", true);
                dugongHelper2.GetComponent<Animator>().SetBool("Idle", true);

                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(2f);
                    dugongHelper1.GetComponent<Animator>().SetTrigger("Celebrate");
                    dugongHelper2.GetComponent<Animator>().SetTrigger("Celebrate");

                }

                UIManager.instance.EnablePlayerMovement();
                //activate infos
                firstCharacterSet.SetActive(false);

                secondCharacterSet.SetActive(true);
                ParticleManager.instance.SpawnPuffParticle(secondCharacterSet.transform.position);

            }

        }

        //QuestTalkBeachHelpers
        if (questName == "Find out about the Dugong")
        {
         
        }
        //QuestTalkStrandedDugongHelpers
        if(questName == "Find help to save the Dugong")
        {
            Inventory.instance.Add(stick, 1, true);
        }



        //QuestInspectDugong
        //if (questName == "Approach the stranded Dugong")
        //{
        //    //strandedDugong.GetComponent<Outline>().enabled = false;
        //}

        //QuestCleanBeachTrash
        if (questName == "Clean the beach")
        {
            beachTrashToClean.SetActive(false);
            dolphinEntranceTimeline.SetActive(true);
            UIManager.instance.DisablePlayerMovement();

            StartCoroutine(WaitForSecondsForDolphin());
            IEnumerator WaitForSecondsForDolphin()
            {
                yield return new WaitForSeconds(6);
                dolphinEntranceDialogue.TriggerDialogue();

                //wait for cutscene to be over...
                yield return new WaitForSeconds(8);
                UIManager.instance.EnablePlayerMovement();
            }

            //enable character set 3
            secondCharacterSet.SetActive(false);
            thirdCharacterSet.SetActive(true);

            ThirdPersonController.instance.gameObject.SetActive(false);
            ThirdPersonController.instance.gameObject.transform.position = playerRepositionPoint.transform.position;
            ThirdPersonController.instance.gameObject.SetActive(true);

        }

    }

    //private void OnDisable()
    //{
    //    //unsubscribe for optimization
    //    GameEvents.instance.onQuestCompleted -= BeachQuestCompleteCheck;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
