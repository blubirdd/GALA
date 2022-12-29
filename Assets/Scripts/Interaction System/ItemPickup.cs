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
        Inventory.instance.Add(item, 1);
        Destroy(gameObject);
    }
     

}
