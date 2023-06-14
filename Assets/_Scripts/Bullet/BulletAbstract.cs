using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbstract : MyMonoBehaviour
{
    [Header("Bullet Abstract")]
    [SerializeField] protected BulletCtrl bulletCtrl;
    public BulletCtrl DamageSender { get => bulletCtrl; }
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
}
