using UnityEngine;

public class PlayerStats : CharStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChangedCallBack += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment)
    {
        if (newEquipment != null)
        {
            if (newEquipment.phyAtkAdd != 0) phyAtk.AddModifier(ModifierType.modifiersAdd, newEquipment.phyAtkAdd * (newEquipment.phyAtkMulSelf == 1f ? 1 : newEquipment.phyAtkMulSelf));
            if (newEquipment.phyAtkMul != 0) phyAtk.AddModifier(ModifierType.modifiersMul, newEquipment.phyAtkMul);
            if (newEquipment.phyDefAdd != 0) phyDef.AddModifier(ModifierType.modifiersAdd, newEquipment.phyDefAdd * (newEquipment.phyDefMulSelf == 1f ? 1 : newEquipment.phyDefMulSelf));
            if (newEquipment.phyDefMul != 0) phyDef.AddModifier(ModifierType.modifiersMul, newEquipment.phyDefMul);
            if (newEquipment.phyDmgRdc != 0) phyDmgReduce.AddModifier(ModifierType.modifiersAdd, newEquipment.phyDmgRdc);

        }
        if (oldEquipment != null)
        {
            if (oldEquipment.phyAtkAdd != 0) phyAtk.AddModifier(ModifierType.modifiersAdd, oldEquipment.phyAtkAdd * (oldEquipment.phyAtkMulSelf == 1f ? 1 : oldEquipment.phyAtkMulSelf), true);
            if (oldEquipment.phyAtkMul != 0) phyAtk.AddModifier(ModifierType.modifiersMul, oldEquipment.phyAtkMul, true);
            if (oldEquipment.phyDefAdd != 0) phyDef.AddModifier(ModifierType.modifiersAdd, oldEquipment.phyDefAdd * (oldEquipment.phyDefMulSelf == 1f ? 1 : oldEquipment.phyDefMulSelf), true);
            if (oldEquipment.phyDefMul != 0) phyDef.AddModifier(ModifierType.modifiersMul, oldEquipment.phyDefMul, true);
            if (oldEquipment.phyDmgRdc != 0) phyDmgReduce.AddModifier(ModifierType.modifiersAdd, oldEquipment.phyDmgRdc, true);

        }

        Debug.Log("ATK = " + phyAtk.GetValue() + ".  DEF = " + phyDef.GetValue() + ".  DMGRDC = " + phyDmgReduce.GetValue());
    }
}
