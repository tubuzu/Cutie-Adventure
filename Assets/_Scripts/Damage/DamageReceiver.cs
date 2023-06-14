using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class DamageReceiver : MyMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected Collider2D receiverCollider;
    [SerializeField] protected int hp = 1;
    [SerializeField] protected int hpMax = 1;
    [SerializeField] protected bool isDead = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.Reborn();
    }
    protected override void Reset()
    {
        base.Reset();
        this.Reborn();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }
    protected virtual void LoadCollider()
    {
        if (receiverCollider != null) return;
        receiverCollider = GetComponent<Collider2D>();
        if (receiverCollider == null) return;
        this.receiverCollider.isTrigger = true;
        // Debug.Log(transform.name + ": LoadCollider", gameObject);
    }
    public virtual void Reborn()
    {
        this.hp = this.hpMax;
        this.isDead = false;
    }
    public virtual void Add(int deduct)
    {
        this.hp += deduct;
        if (this.hp > this.hpMax) this.hp = this.hpMax;
        this.OnAdd();
    }
    public virtual void Deduct(int deduct)
    {
        if (this.IsDead()) return;
        this.hp -= deduct;
        if (this.hp < 0)
        {
            this.hp = 0;
        }
        this.CheckIsDead();
        this.OnDeduct();
    }
    public virtual bool IsDead()
    {
        return this.hp <= 0;
    }
    protected virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.OnDead();
    }
    protected abstract void OnDead();
    protected virtual void OnDeduct()
    {

    }
    protected virtual void OnAdd()
    {

    }
}
