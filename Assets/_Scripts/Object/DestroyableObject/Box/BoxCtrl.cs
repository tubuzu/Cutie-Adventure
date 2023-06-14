using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCtrl : DestroyableObjecCtrl
{
    [SerializeField] protected BoxAnimation boxAnimation;
    public BoxAnimation BoxAnimation => boxAnimation;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxAnimation();
    }
    protected virtual void LoadBoxAnimation()
    {
        if (this.boxAnimation != null) return;
        this.boxAnimation = transform.Find("Model").GetComponent<BoxAnimation>();
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Box.ToString();
    }
}
