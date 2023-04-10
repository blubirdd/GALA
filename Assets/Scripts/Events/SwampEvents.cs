using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwampEvents : MonoBehaviour
{
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

    [Header("Lake target")]
    [SerializeField] private Transform lakeTarget;

    [Header("Timelines")]
    public GameObject timeline001;
    public GameObject timeline002;

    [Header("Dialouges")]
    public SubtleDialogueTrigger containCrocsSubtleDialogue;
    public SubtleDialogueTrigger afterContainCrocsSubtleDialogue;
    public SubtleDialogueTrigger relocateCrocsSubtleDialogue;
    public SubtleDialogueTrigger releasedCrocodiles;

    void Start()
    {
        GameEvents.instance.onQuestAcceptedNotification += SwampQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += SwampQuestCompleteCheck;
    }

    public void SwampQuestAcceptCheck(string questName)
    {
        //QuestContainCrocodiles
        if(questName == "Contain and capture the crocodiles")
        {
            crocodileQuestAnimals.SetActive(true);


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
                yield return new WaitForSeconds(5f);
                relocateCrocsSubtleDialogue.TriggerDialogue();
                //crocodile2.layer = LayerMask.NameToLayer("Default");
            }


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
            StartCoroutine(WaitForDialogue());

            IEnumerator WaitForDialogue()
            {
                yield return new WaitForSeconds(2f);
                timeline001.SetActive(true);
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
                yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
                Destroy(swampWildlifeRanger);
            }

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
