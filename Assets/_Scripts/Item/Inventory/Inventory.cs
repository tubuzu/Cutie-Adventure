using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

public class Inventory : MyMonoBehaviour
{
    [SerializeField] protected int maxSlot = 70;
    [SerializeField] protected List<ItemInventory> items;
    public List<ItemInventory> Items => items;

    protected override void Awake()
    {
        base.Awake();
        this.items = new List<ItemInventory>();
    }
    protected override void Start()
    {
        base.Start();
    }

    public virtual bool ItemCheck(ItemCode itemCode, int itemCount)
    {
        int totalCount = this.ItemTotalCount(itemCode);
        return totalCount >= itemCount;
    }
    protected virtual int ItemTotalCount(ItemCode itemCode)
    {
        int totalCount = 0;
        foreach (ItemInventory item in this.items)
        {
            if (item.itemProfile.itemCode != itemCode) continue;
            totalCount += item.itemCount;
        }
        return totalCount;
    }

    public virtual bool AddItem(ItemInventory itemInventory)
    {
        int addCount = itemInventory.itemCount;
        ItemProfileSO itemProfile = itemInventory.itemProfile;
        ItemCode itemCode = itemProfile.itemCode;
        ItemType itemType = itemProfile.itemType;

        if (itemType == ItemType.Equipment) return this.AddEquipment(itemInventory);
        return this.AddItem(itemCode, addCount);
    }

    public virtual bool AddEquipment(ItemInventory itemPicked)
    {
        if (this.IsInventoryFull()) return false;
        ItemInventory item = itemPicked.Clone();
        this.items.Add(item);
        return true;
    }

    public virtual bool AddItem(ItemCode itemCode, int addCount)
    {
        ItemProfileSO itemProfile = this.GetItemProfile(itemCode);

        int addRemain = addCount;
        int newCount;
        int itemMaxStack;
        int addMore;
        ItemInventory itemExist;
        for (int i = 0; i < this.maxSlot; i++)
        {
            itemExist = this.GetItemNotFullStack(itemCode);
            if (itemExist == null)
            {
                if (this.IsInventoryFull()) return false;

                itemExist = this.CreateEmptyItem(itemProfile);
                this.items.Add(itemExist);
            }
            newCount = itemExist.itemCount + addRemain;

            itemMaxStack = this.GetMaxStack(itemExist);
            if (newCount >= itemMaxStack)
            {
                addMore = itemMaxStack - itemExist.itemCount;
                addRemain -= addMore;
                newCount = itemExist.itemCount + addMore;
            }
            else
            {
                addRemain -= newCount;
            }
            itemExist.itemCount = newCount;
            if (addRemain < 1) break;
        }
        return true;
    }

    protected virtual bool IsInventoryFull()
    {
        if (this.items.Count >= this.maxSlot) return true;
        return false;
    }

    protected virtual ItemInventory GetItemNotFullStack(ItemCode itemCode)
    {
        foreach (ItemInventory item in this.items)
        {
            if (itemCode != item.itemProfile.itemCode) continue;
            if (this.IsFullStack(item)) continue;
            return item;
        }
        return null;
    }
    protected virtual bool IsFullStack(ItemInventory item)
    {
        if (item == null) return true;
        int maxStack = this.GetMaxStack(item);
        return item.itemCount >= maxStack;
    }
    protected virtual ItemInventory CreateEmptyItem(ItemProfileSO itemP)
    {
        ItemInventory item = new ItemInventory
        {
            itemProfile = itemP,
            maxStack = itemP.defaultMaxStack,
        };
        return item;
    }
    protected virtual int GetMaxStack(ItemInventory item)
    {
        if (item == null) return 0;
        return item.maxStack;
    }

    public virtual ItemInventory GetItemByCode(ItemCode itemCode)
    {
        ItemInventory itemInventory = this.items.Find((item) => item.itemProfile.itemCode == itemCode);
        if (itemInventory == null)
        {
            ItemProfileSO profile = this.GetItemProfile(itemCode);
            this.AddEmptyProfile(itemCode);
            itemInventory = this.GetItemByCode(itemCode);
        }
        return itemInventory;
    }
    protected virtual ItemProfileSO GetItemProfile(ItemCode itemCode)
    {
        var profiles = Resources.LoadAll("Item", typeof(ItemProfileSO));
        foreach (ItemProfileSO profile in profiles)
        {
            if (profile.itemCode != itemCode) continue;
            return profile;
        }
        return null;
    }
    protected virtual bool AddEmptyProfile(ItemCode itemCode)
    {
        ItemProfileSO profile = GetItemProfile(itemCode);
        if (profile == null) return false;
        ItemInventory itemInventory = new ItemInventory
        {
            itemProfile = profile,
            maxStack = profile.defaultMaxStack
        };
        this.items.Add(itemInventory);
        return true;
    }
    public virtual void DeductItem(ItemCode itemCode, int deductCount)
    {
        ItemInventory itemInventory;
        int deduct;
        for (int i = this.items.Count - 1; i >= 0; i--)
        {
            if (deductCount <= 0) break;
            itemInventory = this.items[i];

            if (itemInventory.itemProfile.itemCode != itemCode) continue;

            if (deductCount > itemInventory.itemCount)
            {
                deductCount -= itemInventory.itemCount;
                deduct = itemInventory.itemCount;
            }
            else
            {
                deduct = deductCount;
                deductCount = 0;
            }
            itemInventory.itemCount -= deduct;
        }

        this.ClearEmptySlot();
    }
    protected virtual void ClearEmptySlot()
    {
        ItemInventory itemInventory;
        for (int i = 0; i < this.items.Count; i++)
        {
            itemInventory = this.items[i];
            if (itemInventory.itemCount == 0) this.items.RemoveAt(i);
        }
    }
}
