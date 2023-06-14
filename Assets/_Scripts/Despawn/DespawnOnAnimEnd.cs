using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnAnimEnd : Despawn
{
    protected bool isFxOver = false;
    protected override bool CanDespawn()
    {
        if (this.isFxOver) return true;
        return false;
    }
    protected virtual void OnFxOver()
    {
        this.isFxOver = true;
    }
    public override void DespawnObject()
    {
        FXSpawner.Instance.Despawn(transform);
        this.isFxOver = false;
    }
}
