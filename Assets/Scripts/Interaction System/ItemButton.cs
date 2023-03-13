using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public Equipment equipment;

    UIManager uiManager;
    EquipmentManager equipmentManager;
    private void Start()
    {
        uiManager = UIManager.instance;
        equipmentManager = EquipmentManager.instance;
    }

    public void UseEquipment()
    {
        equipment.Use();
    }

    public void UnequipEquipment()
    {
        equipmentManager.Unequip(0);
        uiManager.DisableUnequipButton();
    }
 
}
