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

    TimeController timeController;
    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
        timeController = TimeController.instance;
       
    }
    public bool Interact(Interactor interactor)
    {
        //sleep code here
        timeController.SetTimeOfDay(_timeToTransition);
        return true;
    }


}
