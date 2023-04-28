using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    public SubtleDialogueTrigger subtleDialogue;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    public Animator animator;

    private bool isOpen = false;

    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
        animator = GetComponent<Animator>();
    }
    public bool Interact(Interactor interactor)
    {
        if (!Task.instance.tasksCompeleted.Contains("QuestIntroPart2"))
        {
            subtleDialogue.TriggerDialogue();
            return false;
        }
        isOpen = !isOpen;

        if (isOpen)
        {
            animator.SetBool("isOpen", true);
            _prompt = "Use Door";
        }

        else
        {
            animator.SetBool("isOpen", false);
            _prompt = "Use Door";
        }

        return true;
    }

}
