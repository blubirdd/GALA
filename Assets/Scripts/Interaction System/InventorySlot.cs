using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
  
    public Image icon;
    public TextMeshProUGUI textAmount;

    [Header("Item Details Content")]
    public GameObject itemDetails;
    public Image itemDetailsBackgroundImage;

    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Button useButton;
    public Button throwButton;

    Item item;

    public InventoryUI inventoryUI;


    [Header("Other")]
    public GameObject itemNamePanel;
    public TextMeshProUGUI itemNameText;

    public bool displayItemCount = false;

    //public void AddItem(Item newItem)
    //{
    //    item = newItem;


    //    icon.sprite = item.icon;
    //    icon.enabled = true;
    //    textAmount.enabled = true;
    //    textAmount.SetText(item.itemAmount.ToString());
    //}

    public void AddItem(Item newItem, int amount)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        if (displayItemCount)
        {
            textAmount.enabled = true;
        }


        textAmount.SetText(amount.ToString());
        itemNamePanel.SetActive(true);
        itemNameText.SetText(item.name);
    }
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        textAmount.enabled = false;
        itemNamePanel.SetActive(false);
    }

    public void OnRemoveButton()
    {
      Inventory.instance.Remove(item);
    }
    public void UseItem()
    {
        if(item != null)
        {
            itemDetails.SetActive(true);
            //itemDetailsBackgroundImage.color =  new Color32(255, 255, 255, 255);

            //setup
            itemIcon.sprite = item.icon;
            itemName.text = item.name;
            itemDescription.text = item.itemDescription;
            inventoryUI.currentItem = item;

            //buttons
            if (item.isUsable)
            {
                useButton.gameObject.SetActive(true);
            }

            else
            {
                useButton.gameObject.SetActive(false);
            }

            if (item.isThrowable)
            {
                throwButton.gameObject.SetActive(true);
            }

            else
            {
                throwButton.gameObject.SetActive(false);
            }
        }
    }

    //public void ConsumeItem()
    //{
    //    use the item

    //    itemDetailsBackgroundImage.color = new Color32(116, 91, 91, 255);
    //    itemDetails.SetActive(false);
    //     Inventory.instance.Remove(item);
    //}
}
