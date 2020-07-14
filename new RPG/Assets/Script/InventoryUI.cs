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
                    inventorySlots[i].ClearSlot();
            }
        }
    }

    public void UpdateEquipmentsUI(Equipment newEquipment, Equipment oldEquipment)
    {
        if (newEquipment != null)
        {
            switch (newEquipment.equipmentType)
            {
                case EquipmentType.Head:
                    equipmentSlots[(int)EquipmentTypeSlot.Head].AddItem(newEquipment);
                    break;
                case EquipmentType.Chest:
                    equipmentSlots[(int)EquipmentTypeSlot.Chest].AddItem(newEquipment);
                    break;
                case EquipmentType.Legs:
                    equipmentSlots[(int)EquipmentTypeSlot.Legs].AddItem(newEquipment);
                    break;
                case EquipmentType.Shoes:
                    equipmentSlots[(int)EquipmentTypeSlot.Shoes].AddItem(newEquipment);
                    break;
                case EquipmentType.Waist:
                    equipmentSlots[(int)EquipmentTypeSlot.Waist].AddItem(newEquipment);
                    break;
                case EquipmentType.Hand:
                    equipmentSlots[(int)EquipmentTypeSlot.Hand].AddItem(newEquipment);
                    break;
                case EquipmentType.Weapon:
                    equipmentSlots[(int)EquipmentTypeSlot.Weapon].AddItem(newEquipment);
                    break;
                case EquipmentType.OffHand:
                    equipmentSlots[(int)EquipmentTypeSlot.OffHand].AddItem(newEquipment);
                    break;
                case EquipmentType.Necklace:
                    equipmentSlots[(int)EquipmentTypeSlot.Necklace].AddItem(newEquipment);
                    break;
                case EquipmentType.Ring:
                    equipmentSlots[(int)EquipmentTypeSlot.Ring].AddItem(newEquipment);
                    break;
                case EquipmentType.Ring2:
                    equipmentSlots[(int)EquipmentTypeSlot.Ring2].AddItem(newEquipment);
                    break;
                default:
                    break;
            }
        }
        else if(oldEquipment != null)
        {
            switch (oldEquipment.equipmentType)
            {
                case EquipmentType.Head:
                    equipmentSlots[(int)EquipmentTypeSlot.Head].ClearSlot();
                    break;
                case EquipmentType.Chest:
                    equipmentSlots[(int)EquipmentTypeSlot.Chest].ClearSlot();
                    break;
                case EquipmentType.Legs:
                    equipmentSlots[(int)EquipmentTypeSlot.Legs].ClearSlot();
                    break;
                case EquipmentType.Shoes:
                    equipmentSlots[(int)EquipmentTypeSlot.Shoes].ClearSlot();
                    break;
                case EquipmentType.Waist:
                    equipmentSlots[(int)EquipmentTypeSlot.Waist].ClearSlot();
                    break;
                case EquipmentType.Hand:
                    equipmentSlots[(int)EquipmentTypeSlot.Hand].ClearSlot();
                    break;
                case EquipmentType.Weapon:
                    equipmentSlots[(int)EquipmentTypeSlot.Weapon].ClearSlot();
                    break;
                case EquipmentType.OffHand:
                    equipmentSlots[(int)EquipmentTypeSlot.OffHand].ClearSlot();
                    break;
                case EquipmentType.Necklace:
                    equipmentSlots[(int)EquipmentTypeSlot.Necklace].ClearSlot();
                    break;
                case EquipmentType.Ring:
                    equipmentSlots[(int)EquipmentTypeSlot.Ring].ClearSlot();
                    break;
                case EquipmentType.Ring2:
                    equipmentSlots[(int)EquipmentTypeSlot.Ring2].ClearSlot();
                    break;
                default:
                    break;
            }

        }

    }
}
