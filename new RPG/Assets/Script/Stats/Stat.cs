using System.Collections.Generic;
using UnityEngine;

public enum ModifierType { modifiersAdd, modifiersMul}

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    List<int> modifiersAdd = new List<int>();
    List<float> modifiersMul = new List<float>();

    public void SetBaseValue(int bValue)
    {
        baseValue = bValue;
    }

    public int GetValue()
    {
        int valueAdded = baseValue;
        foreach (int modifierAdd in modifiersAdd)
        {
            valueAdded += modifierAdd;
        }
        float valueToMultiply = 0;
        foreach (float modifierMul in modifiersMul)
        {
            valueToMultiply += modifierMul;
        }
        valueAdded = (int)(valueAdded * (1 + (valueToMultiply / 100f)));
        return Mathf.RoundToInt(valueAdded);
    }

    public void AddModifier(ModifierType modifierType, float modifierValue, bool isRemoving = false)
    {
        if (modifierValue != 0)
        {
            switch (modifierType)
            {
                case ModifierType.modifiersAdd:
                    if (!isRemoving)
                    {
                        modifiersAdd.Add((int)modifierValue);
                    }
                    else
                    {
                        modifiersAdd.Remove((int)modifierValue);
                    }
                    break;
                case ModifierType.modifiersMul:
                    if (!isRemoving)
                    {
                        modifiersMul.Add(modifierValue);
                    }
                    else
                    {
                        modifiersMul.Remove(modifierValue);
                    }
                    break;
                default:
                    break;
            }
        }
    }

}
