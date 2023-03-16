using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterTrap : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
    }

    public bool Interact(Interactor interactor)
    {

       this.gameObject.SetActive(false);
       return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
