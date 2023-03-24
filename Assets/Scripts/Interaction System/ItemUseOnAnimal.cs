using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemUseOnAnimal : MonoBehaviour, IInteractable
{

    public Equipment item;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }


    [SerializeField] private string _prompt = "Use (change this) ";
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _feedIcon;
    //[SerializeField] private bool consumeItem = false;

    [SerializeField] GameObject particle;

    [Header("DIALOGUE")]
    [SerializeField] private SubtleDialogueTrigger dialogue;

    [Header("Food ITEM")]
    [SerializeField] private Equipment food;

    [Header("Settings")]
    public bool canMoveAfterHeal = true;

    ThirdPersonController thirdPersonController;
    EquipmentManager equipmentManager;
    Inventory inventory;
    Animal animal;

    private bool isPickedUP;
    void Start()
    {
        _icon = item.icon;
        InteractionPrompt = _prompt;
        icon = _icon;

        thirdPersonController = ThirdPersonController.instance;
        equipmentManager = EquipmentManager.instance;
        inventory = Inventory.instance;
        animal = GetComponent<Animal>();

    }
    public bool Interact(Interactor interactor)
    {
        //item requirement here
        if (item == equipmentManager.currentEquipment[0])
        {
            if(animal.isInjured)
            {
                UseItem();

                //IF ANIMAL IS PICKABLE
                if (animal.canBePickedUp)
                {
                    //to change to pickup icon
                    interactor._interactionPromptUI.Setup("Pick up animal", _feedIcon);
                }

                EquipmentManager.instance.Unequip(0);
                UIManager.instance.DisableUnequipButton();

                return true;
            }

        }

        if (animal.canBePickedUp && !animal.isInjured && isPickedUP == false)
        {
            PickUpAnimal();

            interactor._interactionPromptUI.Setup("Drop animal", _feedIcon);
            return true;
        }

        if (isPickedUP)
        {
            DropAnimal();
        }

        if(food == equipmentManager.currentEquipment[0])
        {
            interactor._interactionPromptUI.Setup("Feed animal", _feedIcon);
            FeedAnimal();
        }

        else
        {
            if(dialogue != null)
            {
                dialogue.TriggerDialogue();
            }

        }



        return true;
    }

    void UseItem()
    {
        Debug.Log("Used " + item.name);
        inventory.ItemUsed(item);
        thirdPersonController.ItemPickupAnim();

        animal.HealAnimal();

        if(canMoveAfterHeal)
        {
            animal.ActivateAnimal();
        }

        if (particle != null)
        {
            GameObject g = Instantiate(particle, transform.position, Quaternion.identity);
            //Destroy(g, 3f);
        }

        //ParticleManager.instance.SpawnPuffParticle(this.transform.position);
    }

    void PickUpAnimal()
    {
        Debug.Log("PICKING UP ANIMAL");
        isPickedUP = true;
        ThirdPersonController.instance.Carry();


        //disable animal dependecies
        //animal.GetComponent<CapsuleCollider>().enabled = false;
        animal.GetComponent<SphereCollider>().enabled = false;
        animal.GetComponent<NavMeshAgent>().enabled = false;
        animal.DeactivateAnimal();

        //put animal in hand
        animal.transform.SetParent(ThirdPersonController.instance.rightHand);
        animal.transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);


        //this.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    void DropAnimal()
    {
        isPickedUP = false;
        transform.SetParent(null);

        //enable animal dependecies
        animal.GetComponent<SphereCollider>().enabled = true;
        animal.GetComponent<NavMeshAgent>().enabled = true;
        animal.ActivateAnimal();

        ThirdPersonController.instance.CarryStop();
        Debug.Log("Drop animal");
    }

    void FeedAnimal()
    {
        Debug.Log("Feeding");
        thirdPersonController.ItemPickupAnim();
    }


}
