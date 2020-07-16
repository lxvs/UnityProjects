using System;
using UnityEngine;

public enum EquipmentType { Head, Chest, Legs, Shoes, Waist, Hand, Weapon, OffHand, Necklace, Ring} 
public enum EquipmentTypeSlot { Undefined, Head, Necklace, Weapon, Chest, OffHand, Hand, Legs, Waist, Ring, Shoes, Ring2, Unequipped}
public enum EquipmentMeshRegions { Legs, Arms, Torso}

[CreateAssetMenu(fileName = "New equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType equipmentType;
    public EquipmentTypeSlot equipmentSlot;
    public EquipmentMeshRegions[] equipmentMeshRegions;
    public SkinnedMeshRenderer mesh;

    public int hpmAdd = 0;
    public int hpmMul = 0;

    public int phyAtkAdd = 0;
    public int phyDefAdd = 0;
    public int phyAtkMulSelf = 0;
    public int phyDefMulSelf = 0;
    public int phyAtkMul = 0;       // * Σ x %
    public int phyDefMul = 0;
    public int phyDmgRdc = 0;
    public int phyDmgRdcRate = 0;

    public int hitRate = 0;         // %
    public int evadeRate = 0;
    public int critRate = 0;

    public int movementSpeedAdd = 0;    // x %, default player movement speed = 600.
    public int movementSpeedMul = 0;
    public int attackSpeedAdd = 0;
    public int attackSpeedMul = 0;

    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this);
    }

    public void Unequip(Equipment equipment)
    {
        if (equipment != null)
        {
            EquipmentManager.instance.Unequip(equipment);
        }
    }

    public EquipmentTypeSlot EquipmentTypeToSlot()
    {
        switch (equipmentType)
        {
            case EquipmentType.Head:
                equipmentSlot = EquipmentTypeSlot.Head;
                break;
            case EquipmentType.Chest:
                equipmentSlot = EquipmentTypeSlot.Chest;
                break;
            case EquipmentType.Legs:
                equipmentSlot = EquipmentTypeSlot.Legs;
                break;
            case EquipmentType.Shoes:
                equipmentSlot = EquipmentTypeSlot.Shoes;
                break;
            case EquipmentType.Waist:
                equipmentSlot = EquipmentTypeSlot.Waist;
                break;
            case EquipmentType.Hand:
                equipmentSlot = EquipmentTypeSlot.Hand;
                break;
            case EquipmentType.Weapon:
                equipmentSlot = EquipmentTypeSlot.Weapon;
                break;
            case EquipmentType.OffHand:
                equipmentSlot = EquipmentTypeSlot.OffHand;
                break;
            case EquipmentType.Necklace:
                equipmentSlot = EquipmentTypeSlot.Necklace;
                break;
            case EquipmentType.Ring:
                equipmentSlot = EquipmentManager.instance.currentEquipment[(int)EquipmentTypeSlot.Ring] != null ? EquipmentTypeSlot.Ring2 : EquipmentTypeSlot.Ring;
                break;
            default:
                equipmentSlot = 0;
                break;
        }
        return equipmentSlot;
    }
}
