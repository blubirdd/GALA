using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    //public GameObject prefab;

    public override void Use()
    {
        base.Use();

        //equip the item
        EquipmentManager.instance.Equip(this);

        //decrease amount value
        Inventory.instance.DecreaseItemAmountByOne(this);
        

        //remove from inventory
        //RemoveFromInventory();
    }
}

public enum EquipmentSlot {Hand, Chest}
