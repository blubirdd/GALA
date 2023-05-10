using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizNPC : MonoBehaviour, IInteractable
{

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    [Header("Dialgoue")]
    [SerializeField] private DialogueTrigger _dialogue;
    [SerializeField] private DialogueTrigger afterDialogue;

    [Header("Quiz")]
    [SerializeField] private GameObject _quizCanvas;
    [SerializeField] private Transform _benchLocation;

    [Header("If has quiz quest, else leave null")]
    [SerializeField] private GameObject questManager;
    [SerializeField] private string questID;
    public QuestNew quest { get; set; }


    UIManager uiManager;
    ThirdPersonController thirdPersonController;
    Character character;

    [Header("Quest prerequisite")]
    [SerializeField] private string prerequisiteQuest;
    [SerializeField] private SubtleDialogueTrigger prerequieSiteSubtleDialogue;
    private void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;

        //cache
        uiManager = UIManager.instance;
        thirdPersonController = ThirdPersonController.instance;

        character = GetComponent<Character>();
  
    }
    public bool Interact(Interactor interactor)
    {
        if (prerequisiteQuest == "" || Task.instance.tasksCompeleted.Contains(prerequisiteQuest))
        {
            _dialogue.TriggerDialogue();
            PositionPlayer(interactor);

            if (character != null)
            {
                TalkEvents.CharacterApproach(character);
            }
            StartCoroutine(WaitForDialogueEnd());

            if (Task.instance.tasksCompeleted.Contains(questID))
            {
                if (afterDialogue != null)
                {
                    afterDialogue.TriggerDialogue();
                }
            }
        }

        else
        {
            prerequieSiteSubtleDialogue.TriggerDialogue();
        }

        return true;
    }

    public void PositionPlayer(Interactor interactor)
    {
        interactor.gameObject.SetActive(false);
        interactor.transform.position = transform.position + transform.forward * 2;
        interactor.transform.LookAt(transform);
        interactor.gameObject.SetActive(true);
    }

    IEnumerator WaitForDialogueEnd()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        uiManager.DisablePlayerMovement();


        thirdPersonController.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();
        thirdPersonController.transform.rotation = _benchLocation.rotation;
        thirdPersonController.transform.position = _benchLocation.position;
        thirdPersonController.gameObject.SetActive(true);
        EnterQuizState();

        if(questManager != null)
        {
            quest = (QuestNew)questManager.AddComponent(System.Type.GetType(questID));
        }
       

    }
    public void EnterQuizState()
    {
        CinemachineManager.instance._cams[6].SetActive(true);

        thirdPersonController.SitDown();

        _quizCanvas.SetActive(true);
    }
}
