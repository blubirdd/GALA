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
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Button useButton;
    

    Item item;




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
        textAmount.enabled = true;
        textAmount.SetText(amount.ToString());
    }
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        textAmount.enabled = false;
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
            itemDetailsBackgroundImage.color =  new Color32(255, 255, 255, 255);

            itemName.text = item.name;
            
           
        }
    }

    public void ConsumeItem()
    {
        //use the item
        item.Use();

        itemDetailsBackgroundImage.color = new Color32(116, 91, 91, 255);
        itemDetails.SetActive(false);
        // Inventory.instance.Remove(item);
    }
}
