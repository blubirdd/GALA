using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNPC : MonoBehaviour, ICharacter, IInteractable
{
    
    [SerializeField] private string id;
    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;


    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }
    public string npcName { get; set; }


    [Header("Location Target")]
    [SerializeField] private Transform locationTarget;


    [Header("Dialogues")]
    [SerializeField] private DialogueTrigger _dialogue;

    [Header("Others")]
    [SerializeField] private GameObject questMarker;

    AnimalNav navigation;
    Animator animator;
    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
        npcName = id;


        animator = GetComponent<Animator>();
        navigation = GetComponent<AnimalNav>();
    }

    public bool Interact(Interactor interactor)
    {
        _dialogue.TriggerDialogue();

        StartCoroutine(WaitUntilDialogueEnds());

        IEnumerator WaitUntilDialogueEnds()
        {
            yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);

            questMarker.SetActive(false);
            GotoLocation(locationTarget, "Push");
            TalkEvents.CharacterApproach(this);
        }


        return true;
    }

    public void GotoLocation(Transform location, string action)
    {
        navigation.TargetLocation(location);


        animator.SetBool("Run", true);

        StartCoroutine(WaitUntilTargetIsReached());

        IEnumerator WaitUntilTargetIsReached()
        {
            yield return new WaitUntil(() => navigation.targetReached == true);

            navigation.TargetLocation(null);
            animator.SetBool("Run", false);
            //Do action
            animator.SetBool(action, true);
            Debug.Log(action);
        }
    }

    public void GoIdle()
    {
        animator.SetBool("Idle", true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
