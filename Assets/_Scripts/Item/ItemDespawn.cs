using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDespawn : Despawn
{
    public override void DespawnObject()
    {
        ItemDropSpawner.Instance.Despawn(transform.parent);
    }

    protected override bool CanDespawn()
    {
        return false;
    }
}
