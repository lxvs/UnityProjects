using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Button slotButton;
    public Button removeButton;
    public Image icon;

    public void AddItem(Item newItem)
    {

        item = newItem;

        slotButton.interactable = true;
        removeButton.interactable = true;
        icon.sprite = newItem.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        slotButton.interactable = false;
        removeButton.interactable = false;
        icon.sprite = null;           // add this will disable this function, why??
        icon.enabled = false;

    }

    public void OnRemoveButtonClick()
    {
        if (item != null) Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null) item.Use();
    }
}
