using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    [Header("Item Details")]

    public GameObject itemContents;
    public GameObject itemDetailsParent;
    public Image itemDetailsBackgroundImage;

    Inventory inventory;
    Book book;
    InventorySlot[] slots;

    public Item currentItem;

    [Header("Rewards collections")]
    public TextMeshProUGUI goldCoinsCollectedText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI numberOfPhotographsCollectedText;

    void Start()
    {
        inventory = Inventory.instance;
        book = Book.instance;

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

        goldCoinsCollectedText.text = inventory.goldCoins.ToString();
        scoreText.text = inventory.naturePoints.ToString();
        numberOfPhotographsCollectedText.text = book.photosInventory.Count.ToString();
    }

    public void CloseInventory()
    {
        //itemDetailsBackgroundImage.color = new Color32(116, 91, 91, 255);
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
