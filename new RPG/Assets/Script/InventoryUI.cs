using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform equipmentsParent;

    Inventory inventory;
    EquipmentManager equipmentManager;
    InventorySlot[] inventorySlots;
    EquipmentSlot[] equipmentSlots;

    private void Start()
    {
        inventory = Inventory.instance;
        equipmentManager = EquipmentManager.instance;
        inventory.onItemsChangedCallBack += UpdateInventoryUI;
        equipmentManager.onEquipmentChangedCallBack += UpdateEquipmentsUI;
        inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();
        equipmentSlots = equipmentsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventorySlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                if (inventorySlots[i].item != null)
                {
                    inventorySlots[i].ClearSlot();

                }
            }
        }
    }

    public void UpdateEquipmentsUI(Equipment newEquipment, Equipment oldEquipment)
    {
        if (newEquipment != null)
        {
            EquipmentTypeSlot equipmentSlot = newEquipment.equipmentSlot;
            equipmentSlots[(int)equipmentSlot].AddItem(newEquipment);
        }
        else if(oldEquipment != null)
        {
            EquipmentTypeSlot equipmentSlot = oldEquipment.equipmentSlot;
            equipmentSlots[(int)equipmentSlot].ClearSlot();

        }

    }
}
