using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;
    
    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;

        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(true);

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.container.Count)
            {
                slots[i].AddItem(inventory.container[i].item, inventory.container[i].amount);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
         
    }

    public void OpenInventory()
    {
        //inventoryUI.SetActive(!inventoryUI.activeSelf);
        inventoryUI.SetActive(true);
        UIManager.instance.DisableButtonsUIPACK();
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
        UIManager.instance.EnableButtonsUIPACK();
    }


}
