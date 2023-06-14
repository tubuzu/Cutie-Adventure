using System;
// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class LightningAnimation : MyMonoBehaviour
{
    [SerializeField] public Animator animator;
    public event Action OnLightningEnd;

    protected override void Awake()
    {
        base.Awake();
        this.animator = transform.GetComponent<Animator>();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if (animator != null) return;
        this.animator = GetComponent<Animator>();
    }

    protected virtual void OnEnd()
    {
        OnLightningEnd?.Invoke();
    }
}
