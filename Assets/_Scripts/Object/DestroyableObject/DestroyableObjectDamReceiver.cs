using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjectDamReceiver : DamageReceiver
{
    [Header("Destroyable Object DamageReceiver")]
    [SerializeField] protected DestroyableObjecCtrl destroyableObjecCtrl;
    public DestroyableObjecCtrl DestroyableObjecCtrl { get => destroyableObjecCtrl; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDestroyableObjecCtrl();
    }
    protected virtual void LoadDestroyableObjecCtrl()
    {
        if (this.destroyableObjecCtrl != null) return;
        this.destroyableObjecCtrl = transform.parent.GetComponent<DestroyableObjecCtrl>();
        Debug.Log(transform.name + ": LoadDestroyableObjecCtrl", gameObject);
    }
    protected override void OnDead()
    {
        this.OnDeadFX();
        this.OnDeadDrop();
        this.destroyableObjecCtrl.Despawn.DespawnObject();
    }
    protected virtual void OnDeadDrop()
    {
        ItemDropSpawner.Instance.Drop(this.destroyableObjecCtrl.DestroyableObject.dropList, transform.parent.position, Quaternion.identity);
    }
    protected virtual void OnDeadFX()
    {
        string fxName = this.GetOnDeadFXName();
        Transform fxOnDead = FXSpawner.Instance.Spawn(fxName, transform.position, transform.rotation);
        fxOnDead.gameObject.SetActive(true);
    }
    protected virtual string GetOnDeadFXName()
    {
        return FXSpawner.BoxExplode;
    }
    public override void Reborn()
    {
        this.hpMax = this.destroyableObjecCtrl.DestroyableObject.hpMax;
        base.Reborn();
    }
}
