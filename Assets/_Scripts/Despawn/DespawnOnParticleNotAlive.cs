using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnParticleNotAlive : Despawn
{
    [SerializeField] protected ParticleSystem particleFX;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadParticleSystem();
    }
    protected virtual void LoadParticleSystem()
    {
        if (this.particleFX != null) return;
        this.particleFX = GetComponent<ParticleSystem>();
    }
    protected override bool CanDespawn()
    {
        if (!this.particleFX.IsAlive()) return true;
        return false;
    }
    public override void DespawnObject()
    {
        FXSpawner.Instance.Despawn(transform);
    }
}
