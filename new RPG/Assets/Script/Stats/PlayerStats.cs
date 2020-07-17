using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerStats : CharStats
{
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        EquipmentManager.instance.onEquipmentChangedCallBack += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment)
    {
        bool movSpdChanged = false;
        bool hpmChanged = false;
        int hpNow = hp;
        int hpmNow = hpm.GetValue();
        if (oldEquipment != null)
        {
            if (oldEquipment.equipmentType == EquipmentType.Weapon && newEquipment == null)
            {
                atkSpeed.SetBaseValue(100);
            }

            if (oldEquipment.hpmAdd != 0)
            {
                hpm.AddModifier(ModifierType.modifiersAdd, oldEquipment.hpmAdd, true);
                hpmChanged = true;
            }

            if (oldEquipment.hpmMul != 0)
            {
                hpm.AddModifier(ModifierType.modifiersMul, oldEquipment.hpmMul, true);
                hpmChanged = true;
            }

            if (oldEquipment.phyAtkAdd != 0) phyAtk.AddModifier(ModifierType.modifiersAdd, oldEquipment.phyAtkAdd * (1f + oldEquipment.phyAtkMulSelf * .01f), true);
            if (oldEquipment.phyAtkMul != 0) phyAtk.AddModifier(ModifierType.modifiersMul, oldEquipment.phyAtkMul, true);
            if (oldEquipment.phyDefAdd != 0) phyDef.AddModifier(ModifierType.modifiersAdd, oldEquipment.phyDefAdd * (1f + oldEquipment.phyDefMulSelf * .01f), true);
            if (oldEquipment.phyDefMul != 0) phyDef.AddModifier(ModifierType.modifiersMul, oldEquipment.phyDefMul, true);
            if (oldEquipment.hitRate != 0) hitRate.AddModifier(ModifierType.modifiersAdd, oldEquipment.hitRate, true);
            if (oldEquipment.evadeRate != 0) evadeRate.AddModifier(ModifierType.modifiersAdd, oldEquipment.evadeRate, true);
            if (oldEquipment.critRate != 0) critRate.AddModifier(ModifierType.modifiersAdd, oldEquipment.critRate, true);
            if (oldEquipment.phyDmgRdc != 0) phyDmgReduce.AddModifier(ModifierType.modifiersAdd, oldEquipment.phyDmgRdc, true);
            if (oldEquipment.phyDmgRdcRate != 0) phyDmgRdcRate.AddModifier(ModifierType.modifiersAdd, oldEquipment.phyDmgRdcRate, true);
            if (oldEquipment.attackSpeedAdd != 0) atkSpeed.AddModifier(ModifierType.modifiersAdd, oldEquipment.attackSpeedAdd, true);
            if (oldEquipment.attackSpeedMul != 0) atkSpeed.AddModifier(ModifierType.modifiersMul, oldEquipment.attackSpeedMul, true);
            if (oldEquipment.movementSpeedAdd != 0)
            {
                movSpeed.AddModifier(ModifierType.modifiersAdd, oldEquipment.movementSpeedAdd, true);
                movSpdChanged = true;
            }
            if (oldEquipment.movementSpeedMul != 0)
            {
                movSpeed.AddModifier(ModifierType.modifiersMul, oldEquipment.movementSpeedMul, true);
                movSpdChanged = true;
            }
        }

        if (newEquipment != null)
        {
            if (newEquipment.equipmentType == EquipmentType.Weapon)
            {
                EquipmentWeapon newWeapon = (EquipmentWeapon)newEquipment;
                switch (newWeapon.weaponType)
                {
                    case WeaponType.Sword:
                        atkSpeed.SetBaseValue(100);
                        break;
                    case WeaponType.Knife:
                        atkSpeed.SetBaseValue(120);
                        break;
                    case WeaponType.Axe:
                        atkSpeed.SetBaseValue(60);
                        break;
                    case WeaponType.Club:
                        atkSpeed.SetBaseValue(80);
                        break;
                    default:
                        break;
                }
            }

            if (newEquipment.hpmAdd != 0)
            {
                hpm.AddModifier(ModifierType.modifiersAdd, newEquipment.hpmAdd);
                hpmChanged = true;
            }

            if (newEquipment.hpmMul != 0)
            {
                hpm.AddModifier(ModifierType.modifiersMul, newEquipment.hpmMul);
                hpmChanged = true;
            }

            if (newEquipment.phyAtkAdd != 0) phyAtk.AddModifier(ModifierType.modifiersAdd, newEquipment.phyAtkAdd * (1f + newEquipment.phyAtkMulSelf * .01f));
            if (newEquipment.phyAtkMul != 0) phyAtk.AddModifier(ModifierType.modifiersMul, newEquipment.phyAtkMul);
            if (newEquipment.phyDefAdd != 0) phyDef.AddModifier(ModifierType.modifiersAdd, newEquipment.phyDefAdd * (1f + newEquipment.phyDefMulSelf * .01f));
            if (newEquipment.phyDefMul != 0) phyDef.AddModifier(ModifierType.modifiersMul, newEquipment.phyDefMul);
            if (newEquipment.hitRate != 0) hitRate.AddModifier(ModifierType.modifiersAdd, newEquipment.hitRate);
            if (newEquipment.evadeRate != 0) evadeRate.AddModifier(ModifierType.modifiersAdd, newEquipment.evadeRate);
            if (newEquipment.critRate != 0) critRate.AddModifier(ModifierType.modifiersAdd, newEquipment.critRate);
            if (newEquipment.phyDmgRdc != 0) phyDmgReduce.AddModifier(ModifierType.modifiersAdd, newEquipment.phyDmgRdc);
            if (newEquipment.phyDmgRdcRate != 0) phyDmgRdcRate.AddModifier(ModifierType.modifiersAdd, newEquipment.phyDmgRdcRate);
            if (newEquipment.attackSpeedAdd != 0) atkSpeed.AddModifier(ModifierType.modifiersAdd, newEquipment.attackSpeedAdd);
            if (newEquipment.attackSpeedMul != 0) atkSpeed.AddModifier(ModifierType.modifiersMul, newEquipment.attackSpeedMul);
            if (newEquipment.movementSpeedAdd != 0)
            {
                movSpeed.AddModifier(ModifierType.modifiersAdd, newEquipment.movementSpeedAdd);
                movSpdChanged = true;
            }

            if (newEquipment.movementSpeedMul != 0)
            {
                movSpeed.AddModifier(ModifierType.modifiersMul, newEquipment.movementSpeedMul);
                movSpdChanged = true;
            }
        }

        if (movSpdChanged)
        {
            agent.speed = movSpeed.GetValue() / 100f;
        }

        if (hpmChanged)
        {
            hp = (int)((float)hpm.GetValue() * hpNow / hpmNow);
        }

        UIManager.instance.UpdateStats();
    }
}
