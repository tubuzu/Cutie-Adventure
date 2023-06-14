using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class ItemLooter : InventoryAbstract
{
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected Rigidbody _rigidbody;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTrigger();
        this.LoadRigidBody();
    }
    protected virtual void LoadRigidBody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = transform.GetComponent<Rigidbody>();
        this._rigidbody.isKinematic = true;
        this._rigidbody.useGravity = false;
        // Debug.Log(transform.name + " : LoadInventory", gameObject);
    }
    protected virtual void LoadTrigger()
    {
        if(this._collider != null) return;
        this._collider = transform.GetComponent<SphereCollider>();
        this._collider.isTrigger = true;
        this._collider.radius = 0.3f;
        // Debug.Log(transform.name + " : LoadCollider", gameObject);
    }
    protected virtual void OnTriggerEnter(Collider collider)
    {
        ItemPickupable itemPickupable = collider.GetComponent<ItemPickupable>();
        if (itemPickupable == null) return;

        ItemInventory itemInventory = itemPickupable.ItemCtrl.ItemInventory.Clone();
        if(this.inventory.AddItem(itemInventory))
        {
            itemPickupable.Picked();
        }
    }
}
