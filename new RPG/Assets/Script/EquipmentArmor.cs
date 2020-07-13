using UnityEngine;

public enum ArmorType { Leather, Bronze, Steel, BlueSteel, Silver, Golden, Diamond, MonsterHide, FireElement } 
[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Equipment-Armor")]
public class EquipmentArmor : Equipment
{
    public ArmorType armorType;

}
