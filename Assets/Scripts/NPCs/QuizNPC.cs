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

    [Header("Quiz")]
    [SerializeField] private GameObject _quizCanvas;
    [SerializeField] private Transform _benchLocation;

    UIManager uiManager;
    ThirdPersonController thirdPersonController;
    private void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;

        //cache
        uiManager = UIManager.instance;
        thirdPersonController = ThirdPersonController.instance;
  
    }
    public bool Interact(Interactor interactor)
    {
        _dialogue.TriggerDialogue();

        StartCoroutine(WaitForDialogueEnd());
        return true;
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

    }
    public void EnterQuizState()
    {
        CinemachineManager.instance._cams[6].SetActive(true);

        thirdPersonController.SitDown();

        _quizCanvas.SetActive(true);
    }
}
