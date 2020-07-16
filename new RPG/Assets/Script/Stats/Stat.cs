using System.Collections.Generic;
using UnityEngine;

public enum ModifierType { modifiersAdd, modifiersMul}

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    private List<int> modifiersAdd = new List<int>();
    private List<float> modifiersMul = new List<float>();

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
        float valueMuled = valueAdded;
        foreach (float modifierMul in modifiersMul)
        {
            valueMuled *= modifierMul;
        }
        return Mathf.RoundToInt(valueMuled);
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
