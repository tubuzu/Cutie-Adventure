using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAbstract : MyMonoBehaviour
{
    [Header("Inventory Abstract")]
    [SerializeField] protected Inventory inventory;
    public Inventory Inventory { get => inventory; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventory();
    }
    protected virtual void LoadInventory()
    {
        if (this.inventory != null) return;
        this.inventory = transform.parent.GetComponent<Inventory>();
        Debug.Log(transform.name + ": LoadInventory", gameObject);
    }
}
