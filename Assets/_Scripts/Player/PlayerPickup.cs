using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : PlayerAbstract
{
    [Header("Player Pickup")]
    [SerializeField] protected Collider2D _collider;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTrigger();
    }
    protected virtual void LoadTrigger()
    {
        if (this._collider != null) return;
        this._collider = transform.GetComponent<Collider2D>();
        this._collider.isTrigger = true;
        // Debug.Log(transform.name + " : LoadCollider", gameObject);
    }
    public virtual void ItemPickup(ItemPickupable itemPickupable)
    {
        ItemInventory itemInventory = itemPickupable.ItemCtrl.ItemInventory;
        if (this.playerCtrl.Inventory.AddItem(itemInventory))
        {
            itemPickupable.Picked();
            SpawnCollectedEffect(itemPickupable.transform);

        }
    }
    public virtual void ItemExcute(ItemExcutable itemExcutable)
    {
        ItemCode itemCode = itemExcutable.GetItemCode();

        switch (itemCode)
        {
            case ItemCode.Heart:
                this.playerCtrl.PlayerStatus.Add(1);
                AudioManager.Ins.PlaySFX(EffectSound.CollectItemSound);
                SpawnCollectedEffect(itemExcutable.transform);
                break;
            case ItemCode.Star:
                StarCount.Ins.AddStar();
                AudioManager.Ins.PlaySFX(EffectSound.CollectItemSound);
                SpawnCollectedEffect(itemExcutable.transform);
                break;
            case ItemCode.Coin:
                CoinCount.Ins.AddOneCoin();
                AudioManager.Ins.PlaySFX(EffectSound.CoinSound);
                SpawnCollectedEffect(itemExcutable.transform);
                break;
            default:
                break;
        }
        itemExcutable.Picked();
    }

    public virtual void SpawnCollectedEffect(Transform item)
    {
        FXSpawner.Instance.Spawn(FXSpawner.CollectedEffect, item.position, Quaternion.identity);
    }
}
