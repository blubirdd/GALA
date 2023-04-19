using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPC : MonoBehaviour, IInteractable
{
    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    [Header("Tutorial")]
    [SerializeField] private int tutorialID;

    private void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;

    }
    public bool Interact(Interactor interactor)
    {
        TutorialUI.instance.EnableTutorial(tutorialID);
        return true;
    }


}
