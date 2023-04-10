using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ItemUseOnAnimal : MonoBehaviour, IInteractable
{

    public Equipment item;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }


    [SerializeField] private string _prompt = "Use (change this) ";

    [Header("THIS IS ITEM ICON")]
    [SerializeField] private Sprite _icon;
    [Header("ICONS")]

    [SerializeField] private Sprite _pickupIcon;
    [SerializeField] private Sprite _dropIcon;
    //[SerializeField] private bool consumeItem = false;

    [SerializeField] GameObject particle;

    [Header("DIALOGUE")]
    [SerializeField] private SubtleDialogueTrigger dialogue;

    [Header("Settings")]
    public bool canMoveAfterHeal = true;
    public string tagWhenDropped;
    [SerializeField] private GameObject parentWhenDropped;

    ThirdPersonController thirdPersonController;
    EquipmentManager equipmentManager;
    Inventory inventory;
    Animal animal;
    [Header("Settings")]
    public int correctMedkitItem;
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

        if(animal.isInjured == false)
        {
            if (animal.canBePickedUp)
            {
                InteractionPrompt = "Pick up " + animal.photo.name;
                icon = _pickupIcon;
                return;
            }

            else
            {
                InteractionPrompt = "Feed " + animal.photo.name;
                icon = animal.food.icon;
            }
        }

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
                    InteractionPrompt = "Pick up " + animal.photo.name;
                    icon = _pickupIcon;
                    interactor._interactionPromptUI.Setup("Pick up animal", _pickupIcon);
                }

                else
                {
                    interactor._interactionPromptUI.Setup("Feed animal", animal.food.icon);
                    InteractionPrompt = "Feed " + animal.photo.name;
                    icon = animal.food.icon;
                }

                EquipmentManager.instance.Unequip(0);
                UIManager.instance.DisableUnequipButton();

                return true;
            }

        }


        if (animal.food == equipmentManager.currentEquipment[0] && !isPickedUP)
        {
            FeedAnimal();
            return true;
        }

        if (animal.canBePickedUp && !animal.isInjured && isPickedUP == false)
        {
            PickUpAnimal();

            interactor._interactionPromptUI.Setup("Drop " + animal.photo.name, _pickupIcon);
            return true;
        }


        if (isPickedUP)
        {
            DropAnimal();
        }


        else
        {
            if(dialogue != null && animal.isInjured)
            {
                dialogue.TriggerDialogue();
            }

        }



        return true;
    }

    void UseItem()
    {
        Debug.Log("Used " + item.name);
        DragManager.instance.EnableMedkit(DragManager.instance.dragSystems[correctMedkitItem]);

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
        //if (animal.canBePickedUp)
        //{
        //    InteractionPrompt = "Pick up animal";
        //    icon = _pickupIcon;
        //}

        //ParticleManager.instance.SpawnPuffParticle(this.transform.position);
    }

    void PickUpAnimal()
    {
        Debug.Log("PICKING UP ANIMAL");
        animal.tag = "Untagged";
        isPickedUP = true;
        ThirdPersonController.instance.Carry();


        //disable animal dependecies
        //animal.GetComponent<CapsuleCollider>().enabled = false;
        animal.GetComponent<SphereCollider>().enabled = false;
        animal.GetComponent<NavMeshAgent>().enabled = false;
        if (animal.chasePlayer)
        {
            animal.animator.enabled = false;
            Vector3 newScale = transform.localScale + new Vector3(-0.2f, -0.2f, -0.2f);
            transform.localScale = newScale;
        }

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

        if(parentWhenDropped != null)
        {
            transform.SetParent(parentWhenDropped.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        animal.tag = tagWhenDropped;

        //enable animal dependecies
        if (animal.chasePlayer)
        {
            Vector3 newScale = transform.localScale + new Vector3(+0.2f, +0.2f, +0.2f);

            StartCoroutine(WaitForSecondsToActivate());
            IEnumerator WaitForSecondsToActivate()
            {
                animal.GetComponent<NavMeshAgent>().enabled = true;

                yield return new WaitForSeconds(5f);
                animal.ActivateAnimal();
                animal.GetComponent<SphereCollider>().enabled = true;

                animal.netPart.SetActive(false);
                animal.ChangeUI(Animal.AnimalStateUI.Idle);
                ParticleManager.instance.SpawnPuffParticle(transform.position);
                animal.animator.enabled = true;
              
                transform.localScale = newScale;
            }
        }

        else
        {
            animal.GetComponent<SphereCollider>().enabled = true;
            animal.GetComponent<NavMeshAgent>().enabled = true;
            animal.ActivateAnimal();
        }

        ThirdPersonController.instance.CarryStop();
        Debug.Log("Drop animal");
    }

    void FeedAnimal()
    {
        
        thirdPersonController.ItemPickupAnim();
        equipmentManager.Unequip(0);
        UIManager.instance.DisableUnequipButton();

        PopupWindow.instance.AddToQueue(animal.food);

        FeedEvents.AnimalFed(animal);
        Debug.Log("Successfully fed " + animal.photo.name);
    }


}
