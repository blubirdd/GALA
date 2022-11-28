using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public Animator animator;

    
    public bool Interact(Interactor interactor)
    {
  
       animator.SetBool("open", true);
       Debug.Log("Chest Opened");

       //disable interaction
       gameObject.layer = default;

       return true;
    }

}
