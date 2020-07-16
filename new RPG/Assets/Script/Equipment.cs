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

    public int phyAtkAdd = 0;
    public int phyDefAdd = 0;
    public float phyAtkMulSelf = 1f;
    public float phyDefMulSelf = 1f;
    public float phyAtkMul = 1f;
    public float phyDefMul = 1f;
    public int phyDmgRdc = 0;

    public float movementSpeedAdd = 0f;
    public float movementSpeedMul = 1f;
    public float attackSpeedAdd = 0f;
    public float attackSpeedMul = 1f;
    //public new short maxStack = 1;
    //public new short maxCarry = 32;

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
