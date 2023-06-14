using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreaksDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        BoxSpawner.Instance.Despawn(transform.parent);
    }
    // protected override void ResetValues()
    // {
    //     base.ResetValues();
    // }
}
