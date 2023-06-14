using System;
using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemBase : ItemAbstract
{
    [Header("Item Base")]
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected float delayPickup = 0.25f;
    protected bool canBePickup = false;
    public static ItemCode String2ItemCode(string itemName)
    {
        try
        {
            return (ItemCode)System.Enum.Parse(typeof(ItemCode), itemName, true);
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e.ToString());
            return ItemCode.NoItem;
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTrigger();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(DelayPickup());
    }

    protected virtual void LoadTrigger()
    {
        if (this._collider != null) return;
        this._collider = transform.GetComponent<Collider2D>();
        this._collider.isTrigger = true;
        // Debug.Log(transform.name + " : LoadCollider", gameObject);
    }
    public virtual ItemCode GetItemCode()
    {
        return ItemPickupable.String2ItemCode(transform.parent.name);
    }
    public virtual void Picked()
    {
        this.itemCtrl.ItemDespawn.DespawnObject();
    }

    IEnumerator DelayPickup()
    {
        yield return new WaitForSeconds(delayPickup);
        this.canBePickup = true;
    }
}
