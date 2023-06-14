using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInventory
{
    public ItemProfileSO itemProfile;
    public int itemCount = 0;
    public int maxStack = 7;
    // public int upgradeLevel = 0;

    public virtual ItemInventory Clone()
    {
        return new ItemInventory
        {
            itemProfile = this.itemProfile,
            itemCount = this.itemCount,
            // upgradeLevel = this.upgradeLevel
        };
    }
}
