using UnityEngine;

public enum WeaponType { Sword, Knife, Axe, Club }
[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Equipment-Weapon")]
public class EquipmentWeapon : Equipment
{
    public WeaponType weaponType;

}
