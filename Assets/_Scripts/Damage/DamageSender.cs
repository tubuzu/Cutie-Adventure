using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageSender : MyMonoBehaviour
{
    [SerializeField] protected Collider2D receiverCollider;
    [SerializeField] protected int damage = 1;
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
    public virtual void Send(Transform obj)
    {
        DamageReceiver damageReceiver;
        damageReceiver = obj.GetComponent<DamageReceiver>();
        if (damageReceiver == null)
            damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
    }
    public virtual void Send(DamageReceiver damageReceiver)
    {
        damageReceiver.Deduct(this.damage);
        OnSendDamage();
    }
    public virtual void OnSendDamage()
    {

    }
    protected virtual void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
