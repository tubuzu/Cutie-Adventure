using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MyMonoBehaviour
{
    [SerializeField] protected Collider2D triggerCollider;
    public Collider2D TriggerCollider => triggerCollider;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTriggerCollider();
        this.LoadAnimator();

    }
    protected virtual void LoadTriggerCollider()
    {
        if (this.triggerCollider != null) return;
        this.triggerCollider = transform.GetComponent<Collider2D>();
    }
    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = transform.GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        this.animator.Play("Start", -1, 0f);
    }
}
