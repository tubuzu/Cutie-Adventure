using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupable : ItemBase
{
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "PlayerPickup" && this.canBePickup)
        {
            Debug.Log("OnTriggerEnter2D: PlayerPickup");
            PlayerCtrl.Ins.PlayerPickup.ItemPickup(this);
        }
    }
}
