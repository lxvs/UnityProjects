using System;
using UnityEngine;

public enum EquipmentType { Head, Chest, Legs, Shoes, Waist, Hand, Weapon, Shield, Necklace, Ring, Ring2 } 

[CreateAssetMenu(fileName = "New equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType equipmentType;
    public SkinnedMeshRenderer mesh;

    public int phyAtkAdd = 0;
    public int phyDefAdd = 0;
    public float phyAtkMulSelf = 1f;
    public float phyDefMulSelf = 1f;
    public float phyAtkMul = 1f;
    public float phyDefMul = 1f;
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
}
