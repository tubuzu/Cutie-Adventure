using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjectAbstract : MyMonoBehaviour
{
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
}
