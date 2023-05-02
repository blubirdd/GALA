using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwampEvents : MonoBehaviour
{
    [Header("Quest Giver")]
    public GameObject questManager;
    private QuestNew quest { get; set; }


    [Header("Quest References")]
    public GameObject crocodileFocus;
    public GameObject crocodile1;
    public GameObject crocodile2;

    public GameObject crocodileQuestAnimals;
    public GameObject netItem;

    public GameObject crocodileRelocateTrigger;

    [Header("Others")]
    public GameObject swampWildlifeRanger;
    public GameObject swampWildlifeRanger2;
    public GameObject rangerCrocodile;
    public Transform crocodilePickupLocation;
    public Transform crocodilePickupLocation2;

    [Header("Display Crocodile")]
    public Animal displayCrocodile;
    [Header("Display Crocodile")]
    public GameObject relocateQuestTrigger;
    [Header("Lake target")]
    [SerializeField] private Transform lakeTarget;

    [Header("Timelines")]
    public GameObject timeline001;
    public GameObject timeline002;

    [Header("Walls")]
    public GameObject relocateCrocodileWall;

    [Header("Dialouges")]
    public SubtleDialogueTrigger containCrocsSubtleDialogue;
    public SubtleDialogueTrigger afterContainCrocsSubtleDialogue;
    public SubtleDialogueTrigger relocateCrocsSubtleDialogue;
    public SubtleDialogueTrigger releasedCrocodiles;

    [Header("Quest Characters")]
    [SerializeField] private Character eggGame;
    [SerializeField] private Character moleGame;

    [Header("Crrocodile food")]
    public Outline meatFoodCollectable;

    [Header("Egg Game")]
    public Transform eggGameRespawnPoint;
    public Transform moleGameRespawnPoint;
    public static bool fromEggGame = false;
    public static bool fromMoleGame = false;
    public GameObject otherEggsParent;

    [Header("Mole Game")]
    public Outline moleGameHouseTrigger;

    [Header("End")]
    public GameObject rainforestLocation;
    void Start()
    {
        GameEvents.instance.onQuestAcceptedNotification += SwampQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += SwampQuestCompleteCheck;

        if (Task.instance.tasksCompeleted.Contains("QuestRelocateCrocodile"))
        {

            relocateCrocodileWall.SetActive(false);
        }

        if (fromEggGame)
        {
            ThirdPersonController.instance.gameObject.SetActive(false);
            ThirdPersonController.instance.gameObject.transform.position = eggGameRespawnPoint.position;
            ThirdPersonController.instance.gameObject.SetActive(true);

            Player.instance.eggGameScore = 10;
            fromEggGame = false;

            if (Task.instance.tasksCompeleted.Contains("QuestFindMoreEggs"))
            {
                quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestFindMoreEggs"));
            }

            PlayerLocationManager.currentLocation = "Swamp";
            PlayerLocationManager.instance.SetCurrentLocationActive();

        }

        if (fromMoleGame)
        {
            ThirdPersonController.instance.gameObject.SetActive(false);
            ThirdPersonController.instance.gameObject.transform.position = moleGameRespawnPoint.position;
            ThirdPersonController.instance.gameObject.SetActive(true);

            Player.instance.moleGameScore = 10;
            if (Task.instance.tasksCompeleted.Contains("QuestTalkSwampBiologist2"))
            {
                quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestTalkSwampBiologist2"));
            }

            fromMoleGame = false;
        }

        Debug.Log("Egg game: " + Player.instance.eggGameScore);
        Debug.Log("Mole game: " + Player.instance.moleGameScore);



        StartCoroutine(WaitForMinigame());
        IEnumerator WaitForMinigame()
        {
            yield return new WaitForSeconds(1);
            if (Player.instance.moleGameScore > 0)
            {
                Debug.Log("trigger mole quest event");
                TalkEvents.CharacterApproach(moleGame);
            }

            if (Player.instance.eggGameScore > 0)
            {
                Debug.Log("trigger mole egg event");
                TalkEvents.CharacterApproach(eggGame);
            }
        }

    }

    private void OnEnable()
    {

    }

    public void SwampQuestAcceptCheck(string questName)
    {
        //QuestContainCrocodiles
        if(questName == "Contain and capture the crocodiles")
        {
            crocodileQuestAnimals.SetActive(true);
            //crocodile1.GetComponent<Outline>().enabled = true;
            //crocodile2.GetComponent<Outline>().enabled = true;
            FocusOnTransform(crocodileFocus.transform, 10f);
            StartCoroutine(WaitToTriggerDialgoue());

            IEnumerator WaitToTriggerDialgoue()
            {
                yield return new WaitForSeconds(2f);
                containCrocsSubtleDialogue.TriggerDialogue();
                yield return new WaitForSeconds(10f);
                netItem.GetComponent<Outline>().enabled = true;

            }
        }
        
        //QuestRelocateCrocodile
        if(questName == "Release crocodiles in the wild")
        {
            //enable pickup
            crocodile1.layer = LayerMask.NameToLayer("Animal");
            crocodile2.layer = LayerMask.NameToLayer("Animal");

            timeline001.SetActive(false);
            timeline002.SetActive(true);

            StartCoroutine(WaitToTriggerDialgoueForRelocation());

            IEnumerator WaitToTriggerDialgoueForRelocation()
            {
                UIManager.instance.DisablePlayerMovement();
                yield return new WaitForSeconds(5f);
                relocateCrocsSubtleDialogue.TriggerDialogue();
                //crocodile2.layer = LayerMask.NameToLayer("Default");
                yield return new WaitForSeconds(6f);
                UIManager.instance.EnablePlayerMovement();
            }

            relocateQuestTrigger.SetActive(true);
        }

        //QuestEnterCrocSanctuary
        if(questName == "Enter the Crocodile Sanctuary")
        {
            relocateCrocodileWall.SetActive(false);
        }

        //QuestFindMoreEggs
        if(questName == "Find more Crocodile Eggs")
        {
            otherEggsParent.SetActive(true);
        }

        //Mole Minigame
        if(questName == "Enter the shelter and complete game")
        {
            moleGameHouseTrigger.enabled = true;
        }

        if(questName== "Learn about the crocodiles")
        {
            meatFoodCollectable.enabled = true;
        }

    }

    public void SwampQuestCompleteCheck(string questName)
    {
        //QuestTalkSwampResident
        if (questName == "Swamp and Marshes Wetlands")
        {
            //completed talk to resident
        }

        if (questName == "Contain and capture the crocodiles")
        {
            //ranger entrance
            afterContainCrocsSubtleDialogue.TriggerDialogue();
            StartCoroutine(WaitForDialogueToContain());

            //crocodile1.GetComponent<Outline>().enabled = false;
            //crocodile2.GetComponent<Outline>().enabled = false; ;
            IEnumerator WaitForDialogueToContain()
            {
                yield return new WaitForSeconds(2f);

                timeline001.SetActive(true);
                UIManager.instance.DisablePlayerMovement();
                yield return new WaitForSeconds(10f);
                UIManager.instance.EnablePlayerMovement();
            }


        }

        //QuestTalkWildlife Ranger
        if (questName == "Talk to the Wildlife Ranger")
        {
            //set crocodile positions
            crocodile1.transform.position = crocodilePickupLocation.position;
            crocodile1.SetActive(false);
            crocodile2.transform.position = crocodilePickupLocation2.position;

        }

        if (questName == "Release crocodiles in the wild")
        {
            displayCrocodile.GetComponent<NavMeshAgent>().enabled = true;
            

            displayCrocodile.ActivateAnimal();
            displayCrocodile.GetComponent<SphereCollider>().enabled = true;

            displayCrocodile.netPart.SetActive(false);
            displayCrocodile.ChangeUI(Animal.AnimalStateUI.Idle);
            ParticleManager.instance.SpawnPuffParticle(transform.position);
            displayCrocodile.animator.enabled = true;

            //deactivate trigger
            crocodileRelocateTrigger.SetActive(false);


            //move to lake
            crocodile2.GetComponent<NavMeshAgent>().SetDestination(lakeTarget.position);
            displayCrocodile.GetComponent<NavMeshAgent>().SetDestination(lakeTarget.position);

            
            //trigger dialogue
            releasedCrocodiles.TriggerDialogue();

            //set active 2nd ranger in enclosure
            swampWildlifeRanger2.SetActive(true);
        }

        //QuestTalkSwampWildlifeRanger2
        if(questName == "Report to Wildlife Ranger")
        {
            StartCoroutine(WaitForDialogue());
            IEnumerator WaitForDialogue()
            {
                yield return new WaitForEndOfFrame();
                yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);

                ParticleManager.instance.SpawnPuffParticle(swampWildlifeRanger.transform.position);
                Destroy(swampWildlifeRanger);
            }

        }

        //QuestTakeSwampQuiz
        if(questName == "Take and pass the swamp quiz")
        {
            rainforestLocation.SetActive(true);
        }

        //QuestPlayMoleGame
        if (questName == "Enter the shelter and complete game")
        {
            moleGameHouseTrigger.enabled = false;
        }

    }

    public void FocusOnTransform(Transform transform, float duration)
    {
        StartCoroutine(WaitForSeconds());
        IEnumerator WaitForSeconds()
        {

            yield return new WaitForEndOfFrame();
            UIManager.instance.DisablePlayerMovement();

            CinemachineManager.instance.lookAtTargetCamera.gameObject.SetActive(true);
            CinemachineManager.instance.lookAtTargetCamera.Follow = transform;
            CinemachineManager.instance.lookAtTargetCamera.LookAt = transform;
            CinemachineManager.instance.lookAtTargetCamera.m_Lens.FieldOfView = 30f;

            yield return new WaitForSeconds(duration);
            CinemachineManager.instance.lookAtTargetCamera.gameObject.SetActive(false);
            CinemachineManager.instance.lookAtTargetCamera.m_Lens.FieldOfView = 60f;

            UIManager.instance.EnablePlayerMovement();


        }
    }
}
