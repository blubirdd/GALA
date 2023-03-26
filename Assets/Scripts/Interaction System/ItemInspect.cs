using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInspect : MonoBehaviour, IInteractable
{
    
    [SerializeField] private string _prompt = "Inspect ";
    [Header("Interaction Interface")]
    [SerializeField] private Sprite _icon;

    public Item item;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [Header("Settings")]
    [SerializeField] private bool isItem = true;

    [Header("Animation")]
    [SerializeField] private float animationTime = 2.5f;
    [SerializeField] private bool playInspectAnimation = true;
    [Header("Revelation")]
    [SerializeField] private bool focusOnTarget = false;
    [SerializeField] private GameObject TargetToFocusOn;


    [Header("Quest")]
    [SerializeField] private bool hasQuest = false;
    [SerializeField] private GameObject questManager;
    [SerializeField] private string questID;
    [SerializeField] private float giveQuestDelay = 0f;

    public bool triggerCharacterApproach = true;
    public bool disableScriptAfterInspect = false;
    private QuestNew quest { get; set; }

    [Header("Dialogue")]
    [SerializeField] private bool hasDialogue = false;
    [SerializeField] private SubtleDialogueTrigger _dialogue;


    ThirdPersonController thirdPersonController;
    UIManager uiManager;
    CinemachineManager cinemachineManager;
    Character character;
    CinemachineTargetGroup cinemachineTargetGroup;
    void Start()
    {
        _icon = item.icon;
        InteractionPrompt = _prompt;
        icon = _icon;
        thirdPersonController = ThirdPersonController.instance;
        uiManager = UIManager.instance;
        cinemachineManager = CinemachineManager.instance;
        //cinemachineTargetGroup = CinemachineTargetGroup.instance;

        character = GetComponent<Character>();
        
    }
    public bool Interact(Interactor interactor)
    {
        PickUp();

        if(hasQuest)
        {
            StartCoroutine(AssignQuest());
        }

        if(hasDialogue)
        {
            _dialogue.TriggerDialogue();
        }

        if(character != null && triggerCharacterApproach)
        {
            TalkEvents.CharacterApproach(character);
        }

        

        return true;
    }

    IEnumerator AssignQuest()
    {
        yield return new WaitForSeconds(giveQuestDelay);
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType(questID));

    }

    void PickUp()
    {
        Debug.Log("Inspected " + item.name);

        //TO CHANGE TO ITEM INPECT ANIM

        if (focusOnTarget)
        {
            //cinemachineTargetGroup.m_Targets[1].target = TargetToFocusOn.transform;
            //cinemachineManager.SwitchToTargetFocusCam();
        }
        else
        {
            if (playInspectAnimation)
            {
                thirdPersonController.InspectAnim();
            }
            
            cinemachineManager.EnableInspectCam();
        }

        Inventory.instance.Add(item, 1, false);

        StartCoroutine(WaitForSeconds(animationTime));

        IEnumerator WaitForSeconds(float delay)
        {
            uiManager.DisablePlayerMovement();

            yield return new WaitForSeconds(delay);

            cinemachineManager.DisableInspectCam();
            uiManager.EnablePlayerMovement();

            //HANDLE DESTROY
            if(isItem)
            {
                //if(focusOnTarget)
                //{
                //    yield return new WaitForSeconds(giveQuestDelay - animationTime);
                //    TargetToFocusOn.GetComponent<Outline>().enabled = true;
                //    Destroy(gameObject);
                //}

                //else
                //{
                //    Destroy(gameObject);
                //}

                yield return new WaitForSeconds(giveQuestDelay - animationTime);



                Destroy(gameObject);
            }

            //if(isItem == false)
            //{
            //    this.gameObject.layer = LayerMask.NameToLayer("Default");
            //}

            if (disableScriptAfterInspect)
            {
                ItemUse itemUse;
                if(this.TryGetComponent(out itemUse))
                {
                    itemUse.enabled = true;
                }
                //this.GetComponent<ItemInspect>().enabled = false;
                Destroy(this.GetComponent<ItemInspect>());

            }

        }

    }
}
