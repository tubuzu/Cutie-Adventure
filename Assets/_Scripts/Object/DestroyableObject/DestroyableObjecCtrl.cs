using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestroyableObjecCtrl : MyMonoBehaviour
{
    [SerializeField] protected Transform model;
    public Transform Model => model;
    [SerializeField] protected Despawn despawn;
    public Despawn Despawn { get => despawn; }
    [SerializeField] protected DestroyableObjectSO destroyableObject;
    public DestroyableObjectSO DestroyableObject { get => destroyableObject; }
    [SerializeField] protected Spawner spawner;
    public Spawner Spawner { get => spawner; }
    [SerializeField] protected DamageReceiver damageReceiver;
    public DamageReceiver DamageReceiver { get => damageReceiver; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadModel();
        this.LoadDespawn();
        this.LoadSO();
        this.LoadDamageReceiver();
    }
    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform?.parent?.parent?.GetComponent<Spawner>();
        Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }
    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }
    protected virtual void LoadDespawn()
    {
        if (this.despawn != null) return;
        this.despawn = transform.GetComponentInChildren<Despawn>();
        Debug.Log(transform.name + ": LoadDespawn", gameObject);
    }
    protected virtual void LoadDamageReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = transform.GetComponentInChildren<DamageReceiver>();
        Debug.Log(transform.name + ": LoadDamageReceiver", gameObject);
    }
    protected virtual void LoadSO()
    {
        if (this.destroyableObject != null) return;
        string resPath = "DestroyableObject/" + this.GetObjectTypeString() + "/" + transform.name;
        Debug.Log(resPath);
        this.destroyableObject = Resources.Load<DestroyableObjectSO>(resPath);
        Debug.Log(transform.name + ": LoadSO" + resPath, gameObject);
    }
    protected abstract string GetObjectTypeString();
}
