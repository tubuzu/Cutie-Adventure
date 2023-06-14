using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MyMonoBehaviour
{
    [SerializeField] protected Transform model;
    public Transform Model { get => model; }
    [SerializeField] protected ItemDespawn itemDespawn;
    public ItemDespawn ItemDespawn { get => itemDespawn; }
    [SerializeField] protected ItemInventory itemInventory;
    public ItemInventory ItemInventory { get => itemInventory; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadJunkDespawn();
    }
    protected override void Reset()
    {
        base.Reset();
        this.itemInventory.itemCount = 1;
        // this.itemInventory.upgradeLevel = 0;
        ItemCode itemCode = ItemCodeParser.FromString(transform.name);
        this.ItemInventory.itemProfile = ItemProfileSO.FindByItemCode(itemCode);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ResetItem();
    }
    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }
    protected virtual void LoadJunkDespawn()
    {
        if (this.itemDespawn != null) return;
        this.itemDespawn = transform.Find("ItemDespawn").GetComponent<ItemDespawn>();
        Debug.Log(transform.name + ": LoadItemDespawn", gameObject);
    }
    public virtual void SetItemIventory(ItemInventory itemI)
    {
        this.itemInventory = itemI.Clone();
    }
    protected virtual void LoadItemInventory()
    {
        if (this.itemInventory.itemProfile != null) return;
        ItemCode itemCode = ItemCodeParser.FromString(transform.name);
        ItemProfileSO itemProfile = ItemProfileSO.FindByItemCode(itemCode);
        this.itemInventory.itemProfile = itemProfile;
        this.ResetItem();
        Debug.Log(transform.name + ": LoadItemInventory", gameObject);
    }

    protected virtual void ResetItem()
    {
        this.itemInventory.itemCount = 1;
        // this.itemInventory.upgradeLevel = 0;
    }
}
