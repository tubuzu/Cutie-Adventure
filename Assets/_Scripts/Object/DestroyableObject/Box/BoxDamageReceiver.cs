using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamageReceiver : DestroyableObjectDamReceiver
{
    // [SerializeField] protected BoxCtrl boxCtrl;
    // public BoxCtrl BoxCtrl { get { return boxCtrl; } }
    // protected override void LoadComponents()
    // {
    //     base.LoadComponents();
    //     this.LoadBoxCtrl();
    // }
    // protected virtual void LoadBoxCtrl()
    // {
    //     this.boxCtrl = transform.parent.GetComponent<BoxCtrl>();
    //     if (this.boxCtrl == null) this.boxCtrl = transform.GetComponent<BoxCtrl>();
    // }

    protected override void OnDead()
    {
        this.Explode();
        base.OnDead();
    }

    protected virtual void Explode()
    {
        string breaksName = BoxSpawner.Box1Breaks;
        if (this.hpMax == 2) breaksName = BoxSpawner.Box2Breaks;
        if (this.hpMax == 3) breaksName = BoxSpawner.Box3Breaks;
        BoxSpawner.Instance.Spawn(breaksName, transform.parent.position, Quaternion.identity);
        CameraShaker.Invoke();
    }

    protected override void OnDeduct()
    {
        base.OnDeduct();
        if (!this.IsDead())
            ((BoxCtrl)this.destroyableObjecCtrl).BoxAnimation.OnHit();
    }
}
