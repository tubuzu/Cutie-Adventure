using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDespawn : Despawn
{
    public override void DespawnObject()
    {
        BoxSpawner.Instance.Despawn(transform.parent);
    }

    protected override bool CanDespawn()
    {
        return false;
    }
}
