using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangeInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt = "";
    [SerializeField] private Sprite _icon;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [SerializeField] private float _timeToTransition;

    [Header("if has quest prerequisite else, leave null")]
    [SerializeField] private string prerequisiteQuest;
    [SerializeField] private SubtleDialogueTrigger _dialogue;
    TimeController timeController;
    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
        timeController = TimeController.instance;
       
    }
    public bool Interact(Interactor interactor)
    {
        if(prerequisiteQuest == "")
        {
            Sleep();
        }

        else
        {
            if (Task.instance.tasksCompeleted.Contains(prerequisiteQuest))
            {
                //sleep code here
                Character character;
                if (TryGetComponent(out character))
                {
                    TalkEvents.CharacterApproach(character);
                }

                Sleep();

            }

            else
            {
                _dialogue.TriggerDialogue();
            }

        }

        return true;

    }

    private void Sleep()
    {
        timeController.SetTimeOfDay(_timeToTransition);
    }

}
