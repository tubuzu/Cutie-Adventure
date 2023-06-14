using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimation : BulletAbstract
{
    [SerializeField] private Animator animator;

    private string currentState;

    private bool broken = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected override void Awake()
    {
        this.animator = transform.GetComponent<Animator>();
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (broken || currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    public void OnBroken()
    {
        ChangeAnimationState(BulletAnimationState.BULLET_BROKEN);
        broken = true;
    }

    public void OnBrokenEnd()
    {
        this.bulletCtrl.BulletDespawn.DespawnObject();
    }
}

public class BulletAnimationState
{
    public static string BULLET_BROKEN = "Broken";
}
