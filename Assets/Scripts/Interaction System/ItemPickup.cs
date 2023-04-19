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
    PopupWindow popupWindow;
    //waypoint
    [Header("If has waypoint on pickup, else leave null")]
    [SerializeField] private Transform _waypointTransform;

    [Header("If has object to activate on pickup, else leave null")]
    [SerializeField] private GameObject objectToActivate;
    void Start()
    {
        _icon = item.icon;
        InteractionPrompt = _prompt + item.name;
        icon = _icon;
        instance = ThirdPersonController.instance;
        popupWindow = PopupWindow.instance;


    }
    public bool Interact(Interactor interactor)
    {
        PickUp();

        return true;
    }

    void PickUp()
    {
        Debug.Log("Picked up " + item.name);
        //popupWindow.AddToQueue("Picked up " + item.name);
        popupWindow.AddToQueuePickedUp(item);

        instance.ItemPickupAnim();
        Inventory.instance.Add(item, 1, true);



        if (_waypointTransform != null)
        {
            //SpawnWaypointMarker();
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        Destroy(gameObject);
    }

    public void SpawnWaypointMarker()
    {
       GameObject waypoint = (GameObject)Instantiate(Resources.Load("WaypointCanvas"));
       waypoint.GetComponent<WaypointUI>().SetTarget(_waypointTransform);
    }
}
