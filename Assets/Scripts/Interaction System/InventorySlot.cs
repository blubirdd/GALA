using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI textAmount;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;


        icon.sprite = item.icon;
        icon.enabled = true;
        textAmount.enabled = true;
        textAmount.SetText(item.itemAmount.ToString());
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
            item.Use();
            
        }
    }
}
