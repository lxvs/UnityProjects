using UnityEngine;

public enum WeaponType { Knife, Axe, Club }
[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Equipment-Armor")]
public class EquipmentWeapon : Equipment
{
    public WeaponType weaponType;

}
