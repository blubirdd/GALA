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

    [Header("Discovery Settings")]
    public bool displayDiscovery = true;

    [Header("Throwable Settings")]
    public bool isThrowable = false;

    [Header("Aiming Settings")]
    public bool isAimable = false;
    public bool isFirable = false;

    [Header("Quest Settings")]
    public bool isAnimlaFood = false;
    [TextArea(3, 10)]
    public string itemDescription;

  
    public virtual void Use()
    {
        Debug.Log("Using " + name);

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
