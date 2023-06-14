using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MyMonoBehaviour
{
    [SerializeField] protected BulletDamageSender bulletDamageSender;
    public BulletDamageSender BulletDamageSender { get => bulletDamageSender; }
    [SerializeField] protected BulletDespawn bulletDespawn;
    public BulletDespawn BulletDespawn { get => bulletDespawn; }

    // [SerializeField] protected  Transform shooter;
    // public Transform Shooter { get => shooter; set => shooter = value; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletDamageSender();
        this.LoadBulletDespawn();
    }
    protected virtual void LoadBulletDamageSender()
    {
        if (this.bulletDamageSender != null) return;
        this.bulletDamageSender = transform.GetComponentInChildren<BulletDamageSender>();
        // Debug.Log(transform.name + ": LoadBulletDamageSender", gameObject);
    }
    protected virtual void LoadBulletDespawn()
    {
        if (this.bulletDespawn != null) return;
        this.bulletDespawn = transform.GetComponentInChildren<BulletDespawn>();
        // Debug.Log(transform.name + ": LoadBulletDespawn", gameObject);
    }
    // public virtual void SetShooter(Transform value)
    // {
    //     this.Shooter = value;
    // }
}
