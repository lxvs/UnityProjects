using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;
    InventorySlot[] slots;

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemsChangedCallBack += UpdateInventoryUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                if (slots[i].item != null)
                    slots[i].ClearSlot();
            }
        }
    }

}
