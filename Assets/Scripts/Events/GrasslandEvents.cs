using BrokenVector.LowPolyFencePack;
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
    [SerializeField] private GameObject idleWildlifeSpecialist2;

    [Header("Prefab to instantiate")]
    [SerializeField] private GameObject tamarawPrefab;

    [Header("First TamarawTrack")]
    //public GameObject tamarawWildAnimalsParent;
    public GameObject firstTamarawTrack;

    [Header("Cutscenes")]
    [SerializeField] private GameObject cureTamarawCutscene;

    [Header("Location")]
    public GameObject riverLocation;

    [Header("GiveItem")]
    public Item itemTogive;

    [Header("Door Border")]
    public DoorController riverDoorBorder;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.onQuestAcceptedForSave += GrasslandQuestAcceptCheck;
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
        if (questName == "Cure the remaining Tamaraws")
        {
            wildTamaraws.SetActive(true);
            //tamarawWildAnimalsParent.SetActive(true);
            //cureTamarawCutscene.SetActive(true);


            //play cutscene here
            //play cutscene
            if (DialogueSystem.instance.dialogueCanvas.activeSelf)
            {
                StartCoroutine(WaitForDialogue());
            }

            IEnumerator WaitForDialogue()
            {
                yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);

                cureTamarawCutscene.SetActive(true);
                UIManager.instance.DisablePlayerMovement();
                //wait for cutscene to display tutorial
                //wait for cutscene to display tutorial
                yield return new WaitForSeconds(12f);

                //display sneak curing tutorial
                TutorialUI.instance.EnableTutorial(4);
                UIManager.instance.EnablePlayerMovement();
                idleWildlifeSpecialist2.SetActive(true);

            }

        }

        if (questName == "Heal the Tamaraw")
        {
            StartCoroutine(WaitUntilDialogueEnd(3, 3f));
        }


        //QuestCollectTamarawTracks
        if (questName == "Tracking the tamaraw")
        {
            firstTamarawTrack.SetActive(true);
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

            StartCoroutine(WaitForDialogue());
            IEnumerator WaitForDialogue()
            {
                yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
                Inventory.instance.Add(itemTogive, 1, true);


            }
        }

        if (questName == "Heal the Tamaraw")
        {
            Destroy(curableTamaraw);

            Instantiate(tamarawPrefab, curableTamaraw.transform.position, Quaternion.identity);

        }



        //QuestTalkWildlifeSpecialist3
        //follow specialist to tamaraw
        if (questName == "Report back to the wildlife specialist")
        {
            wildlifeSpecialist1.SetActive(false);

        }

        //grassland ending
        //QuestTalkWildlifeSpecialistFinal
        if(questName == "Report back to the specialist")
        {
            //tamarawWildAnimalsParent.SetActive(true);

            //ParticleManager.instance.SpawnPuffParticle(wildlifeSpecialist2.transform.position);
        }

        //QuestTalkRiverWildlifeBiologist
        if(questName == "Find out about this area")
        {
            //tamarawWildAnimalsParent.SetActive(false);
        }

        //QuestTakeGrasslandQuiz
        if(questName == "Take and pass the grassland quiz")
        {
            riverLocation.SetActive(true);
            riverDoorBorder.ToggleDoor();
        }

    }

    IEnumerator WaitUntilDialogueEnd(int tutorialID, float delay)
    {
        //yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        yield return new WaitForSeconds(delay);
        TutorialUI.instance.EnableTutorial(tutorialID);
    }

    //private void OnDisable()
    //{
    //    GameEvents.instance.onQuestCompleted -= GrasslandQuestAcceptCheck;
    //    GameEvents.instance.onQuestCompleted -= GrasslandQuestCompleteCheck;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
