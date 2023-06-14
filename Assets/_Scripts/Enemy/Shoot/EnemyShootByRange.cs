using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootByRange : ObjectShootByRange
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    public EnemyCtrl EnemyCtrl { get => enemyCtrl; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }
    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
    }

    protected override void OnShoot()
    {
        base.OnShoot();
        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_ATTACK);
    }

    protected override void OnIdle()
    {
        base.OnIdle();
        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_IDLE);
    }
}
