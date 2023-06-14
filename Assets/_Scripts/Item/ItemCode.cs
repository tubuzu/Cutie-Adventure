using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCode
{
    NoItem = 0,

    Coin = 1,
    Heart = 2,
    Star = 3,
    
}

public class ItemCodeParser
{
    public static ItemCode FromString(string itemName)
    {
        try
        {
            return (ItemCode)System.Enum.Parse(typeof(ItemCode), itemName);
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e.ToString());
            return ItemCode.NoItem;
        }
    }
}
