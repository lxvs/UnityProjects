using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Equipment equipment;
    public Button slotButton;
    public Image icon;

    public void AddItem(Equipment newEquipment) 
    {
        equipment = newEquipment;

        slotButton.interactable = true;
        icon.sprite = newEquipment.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        equipment = null;

        slotButton.interactable = false;
        icon.sprite = null;
        icon.enabled = false;

    }

    public void Unequip()
    {
        if (equipment != null) equipment.Unequip(equipment);
    }
}
