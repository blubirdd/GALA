using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool isUsable = false;
    [Header("Inventory Settings")]
    public bool displayQuantity = false;

    [Header("Discovery Settings")]
    public bool displayDiscovery = true;

    [Header("Throwable Settings")]
    public bool isThrowable = false;

    [Header("Aiming Settings")]
    public bool isAimable = false;
    public bool isFirable = false;

    [Header("Quest Settings")]
    public bool isAnimalFood = false;
    public bool isCustomQuestItem = false;
    public bool isCustomPopup = false;
    [TextArea(3, 10)]
    public string itemDescription;

    public bool isCoin;
    public int value;
  
    public virtual void Use()
    {
        Debug.Log("Using " + name);

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
