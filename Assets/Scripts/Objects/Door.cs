using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;


    public string InteractionPrompt => _prompt;
    public Sprite icon => _icon;

    Animator animator;

    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public bool Interact(Interactor interactor)
    {

        isOpen = !isOpen;

        if (isOpen)
        {
            animator.SetBool("isOpen", true);
            _prompt = "Close Door";
        }

        else
        {
            animator.SetBool("isOpen", false);
            _prompt = "Open Door";
        }

        return true;
    }

}
