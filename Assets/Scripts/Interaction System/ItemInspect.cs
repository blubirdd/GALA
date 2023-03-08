using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInspect : MonoBehaviour, IInteractable
{
    private string _prompt = "Inspect ";
    [SerializeField] private Sprite _icon;
    public Sprite icon => _icon;


    public Item item;


    public string InteractionPrompt => _prompt + item.name;

    ThirdPersonController instance;
    void Start()
    {
        _icon = item.icon;
        instance = ThirdPersonController.instance;
    }
    public bool Interact(Interactor interactor)
    {
        PickUp();
        return true;
    }

    void PickUp()
    {
        Debug.Log("Inspected " + item.name);

        //TO CHANGE TO ITEM INPECT ANIM
        instance.ItemPickupAnim();

        Inventory.instance.Add(item, 1);
        Destroy(gameObject);
    }
}
