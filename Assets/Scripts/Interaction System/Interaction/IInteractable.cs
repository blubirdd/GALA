using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface  IInteractable 
{
    public string InteractionPrompt { get; }
    public Sprite icon { get; }
    public bool Interact(Interactor interactor);



}
