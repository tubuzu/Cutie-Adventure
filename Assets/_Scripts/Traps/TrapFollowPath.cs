using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFollowPath : FollowPath
{
    [SerializeField] protected Animator anim;

    [SerializeField] TrapAnimationState onStopAnim;
    [SerializeField] float stopAnimDuration = 2f / 9f;
    [SerializeField] float stopAnimDelay = 0.25f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if (this.anim != null) return;
        this.anim = transform.GetComponent<Animator>();
    }

    protected override void StopForAWhile()
    {
        if (onStopAnim.ToString() != "None")
            StartCoroutine(OnStopAnim());
        base.StopForAWhile();
    }

    protected virtual IEnumerator OnStopAnim()
    {
        yield return new WaitForSeconds(stopAnimDelay);
        this.anim.Play(onStopAnim.ToString(), -1, 0);
        yield return new WaitForSeconds(stopAnimDuration);
        this.anim.Play(TrapAnimationState.Idle.ToString(), -1, 0);
    }
}

public enum TrapAnimationState
{
    None = 0,
    Idle = 1,
    TopHit = 2,
    BottomHit = 3,
    RightHit = 4,
    LeftHit = 5,
    Blink = 6,
}
