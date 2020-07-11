﻿using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemsChanged();
    public OnItemsChanged onItemsChangedCallBack;

    public short inventorySpace = 32;
    public List<Item> items = new List<Item>();

    /// <summary>
    /// return 0 if added to inventory successfully, 1 if item is not pickable, 2 if inventory is full, 3 if item amount >= maxCarry
    /// </summary>
    /// <param name="newItem"></param>
    /// <returns></returns>
    public short Add(Item newItem)  
    {
        if (!newItem.pickable) 
        {
            //Debug.LogWarning(newItem.name + " is not pickable!");
            return 1;
        }
        
        if (items.Count >= inventorySpace)
        {
            return 2;
        }

        items.Add(newItem);

        if (onItemsChangedCallBack != null) onItemsChangedCallBack.Invoke();
        return 0;

    }

    public short Remove(Item itemToRemove)
    {
        items.Remove(itemToRemove);

        if (onItemsChangedCallBack != null) onItemsChangedCallBack.Invoke();
        return 0;
    }
}