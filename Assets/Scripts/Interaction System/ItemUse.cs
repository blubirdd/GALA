using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour, IInteractable
{




    public Item item;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }


    [SerializeField] private string _prompt = "Use (to change) ";
    [SerializeField] private Sprite _icon;
    //[SerializeField] private bool consumeItem = false;

    [SerializeField] GameObject particle;

    ThirdPersonController thirdPersonController;
    EquipmentManager equipmentManager;
    Inventory inventory;
    void Start()
    {
        _icon = item.icon;
        InteractionPrompt = _prompt;
        icon = _icon;
        thirdPersonController = ThirdPersonController.instance;
        equipmentManager = EquipmentManager.instance;
        inventory = Inventory.instance;
    }
    public bool Interact(Interactor interactor)
    {
        //item requirement here
        if(item == equipmentManager.currentEquipment[0])
        {
            UseItem();
        }

        else
        {
            Debug.Log("Required item not in hand");
        }
        
        return true;
    }

    void UseItem()
    {
        Debug.Log("Used " + item.name);
        inventory.ItemUsed(item);
        thirdPersonController.ItemPickupAnim();

        if (particle != null)
        {
            GameObject g = Instantiate(particle, transform.position, Quaternion.identity);
            //Destroy(g, 3f);
        }
        //ParticleManager.instance.SpawnPuffParticle(this.transform.position);

        //replace item
        ReplaceItemInHand replaceItemInHand;
        if(TryGetComponent(out replaceItemInHand))
        {
            replaceItemInHand.ReplaceItem();
        }
    }
}
