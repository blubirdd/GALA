using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{

    //[SerializeField] private ItemDatabaseObject database;
    /*[SerializeField] private string id;

    [ContextMenu("Generate guid for id")]

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    */

//    private void OnEnable()
//    {
//#if UNITY_EDITOR
//        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));

//#else
//        database = Resources.Load<ItemDatabaseObject>("Database");
//#endif
//    }

    [SerializeField] private string _prompt = "Pick up ";
    [SerializeField] private Sprite _icon;


   
    public Item item;


    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    ThirdPersonController instance;
    void Start()
    {
        _icon = item.icon;
        InteractionPrompt = _prompt + item.name;
        icon = _icon;
        instance = ThirdPersonController.instance;
    }
    public bool Interact(Interactor interactor)
    {
        PickUp();
        return true;
    }

    void PickUp()
    {
        Debug.Log("Picked up " + item.name);

        instance.ItemPickupAnim();
        Inventory.instance.Add(item, 1);
        Destroy(gameObject);
    }
     

}
