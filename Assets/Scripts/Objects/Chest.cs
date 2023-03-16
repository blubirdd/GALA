using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;
        public string InteractionPrompt { get; set; }
        public Sprite icon { get; set; }


    public Animator animator;

    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
    }
    public bool Interact(Interactor interactor)
    {
  
       animator.SetBool("open", true);
       Debug.Log("Chest Opened");

       //disable interaction
       gameObject.layer = default;

       return true;
    }

}
