using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAbstract : MyMonoBehaviour
{
    [SerializeField] protected BoxCtrl boxCtrl;
    public BoxCtrl BoxCtrl { get { return boxCtrl; } }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCtrl();
    }
    protected virtual void LoadBoxCtrl()
    {
        this.boxCtrl = transform.parent.GetComponent<BoxCtrl>();
        if(this.boxCtrl == null) this.boxCtrl = transform.GetComponent<BoxCtrl>();
    }
}
