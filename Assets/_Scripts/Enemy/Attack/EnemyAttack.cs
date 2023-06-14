using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyAbstract
{
    protected bool isHitting = false;
    [SerializeField] protected float delayBeforeHit = 0.2f;
    [SerializeField] protected float hitStartDelay = 6f / 18f;
    [SerializeField] protected float hitDuration = 2f / 18f;
    [SerializeField] protected float hitCountdown = 2f;
    [SerializeField] protected GameObject hitCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHitCollider();
    }

    protected virtual void LoadHitCollider()
    {
        if (this.hitCollider != null) return;
        this.hitCollider = transform.parent.Find("HitBox").gameObject;
        this.hitCollider.gameObject.SetActive(false);
    }

    public virtual void Hit()
    {
        if (isHitting) return;
        StartCoroutine(OnHitDelay());

        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_ATTACK);
        this.enemyCtrl.EnemyAnimation.OnAttacking();

        StartCoroutine(OnHitting());
    }

    IEnumerator OnHitDelay()
    {
        yield return new WaitForSeconds(delayBeforeHit);
    }

    IEnumerator OnHitting()
    {
        isHitting = true;
        yield return new WaitForSeconds(hitStartDelay);
        this.hitCollider.SetActive(true);
        yield return new WaitForSeconds(hitDuration);
        this.hitCollider.SetActive(false);
        this.enemyCtrl.EnemyAnimation.OnAttackingEnd();

        yield return OnHitEnd();
    }

    protected virtual IEnumerator OnHitEnd()
    {
        yield return new WaitForSeconds(hitCountdown);
        isHitting = false;
    }
}
