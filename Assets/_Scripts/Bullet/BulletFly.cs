using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : ParentFly
{
    [SerializeField] protected BulletCtrl bulletCtrl;
    public BulletCtrl BulletCtrl { get => bulletCtrl; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
        this.LoadRigidbody();
    }
    protected virtual void LoadBulletCtrl()
    {
        if (this.bulletCtrl != null) return;
        this.bulletCtrl = transform.parent.GetComponent<BulletCtrl>();
        // Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }
    protected override void LoadRigidbody()
    {
        if (this._rb != null) return;
        this._rb = transform.parent.GetComponent<Rigidbody2D>();
        this._rb.isKinematic = true;
    }

    protected override void ResetValues()
    {
        base.ResetValues();
        this.moveSpeed = 7f;
    }
}
