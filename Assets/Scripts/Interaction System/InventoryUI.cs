using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject itemContents;
    public GameObject itemDetailsParent;
    public Image itemDetailsBackgroundImage;

    Inventory inventory;
    
    InventorySlot[] slots;

    public Item currentItem;


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
        itemDetailsParent.SetActive(true);
        itemContents.SetActive(false);
        UIManager.instance.DisableButtonsUIPACK();
    }

    public void CloseInventory()
    {
        itemDetailsBackgroundImage.color = new Color32(116, 91, 91, 255);
        itemDetailsParent.SetActive(false);
        inventoryUI.SetActive(false);
        UIManager.instance.EnableButtonsUIPACK();
    }

    public void ConsumeItem()
    {
        currentItem.Use();

        CloseInventory();
    }


}
