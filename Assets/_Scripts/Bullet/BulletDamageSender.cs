using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageSender : DamageSender
{
    [SerializeField] protected BulletCtrl bulletCtrl;
    public BulletCtrl BulletCtrl { get => bulletCtrl; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }
    protected virtual void LoadBulletCtrl()
    {
        if (this.bulletCtrl != null) return;
        this.bulletCtrl = transform.parent.GetComponent<BulletCtrl>();
        // Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }
    // public override void OnSendDamage()
    // {
    //     base.OnSendDamage();
    //     this.DestroyBullet();
    // }
    // protected virtual void DestroyBullet()
    // {
    //     this.bulletCtrl.BulletDespawn.DespawnObject();
    // }
}