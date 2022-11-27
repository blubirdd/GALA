using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    private string _prompt = "Pick up ";

    public Item item;
    public string InteractionPrompt => _prompt + item.name;
    public bool Interact(Interactor interactor)
    {
        PickUp();
        return true;
    }

    void PickUp()
    {
        
        Debug.Log("Picked up " + item.name);
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
     

}
