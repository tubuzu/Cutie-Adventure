using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : EnemyAbstract
{
    [SerializeField] private Animator animator;

    private string currentState;

    private List<string> repeatableStates = new List<string> {
        EnemyAnimationState.ENEMY_ATTACK,
    };

    private bool attacking = false;
    public bool Attacking => attacking;

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
        if (this.enemyCtrl.EnemyDamageReceiver.IsDead() && newState != EnemyAnimationState.ENEMY_HIT) return;
        if (attacking && newState != EnemyAnimationState.ENEMY_HIT) return;
        if (currentState == newState && !repeatableStates.Contains(newState)) return;
        animator.Play(newState.ToString(), -1, 0);
        currentState = newState;
    }

    public void OnHit()
    {
        ChangeAnimationState(EnemyAnimationState.ENEMY_HIT);
        if (this.enemyCtrl.EnemyDamageReceiver.IsDead())
        {
            this.enemyCtrl.EnemyMovement.OnDead();
        }
        // beingHit = true;
    }

    public void OnAttacking()
    {
        this.attacking = true;
    }

    public void OnAttackingEnd()
    {
        this.attacking = false;
    }

    // public void OnHitEnd()
    // {
    //     Debug.Log("OnHitEnd");
    //     beingHit = false;

    // }
}

public class EnemyAnimationState
{
    public static string ENEMY_IDLE = "Idle";
    public static string ENEMY_HIT = "Hit";
    public static string ENEMY_RUN = "Run";
    public static string ENEMY_FLY = "Flying";
    public static string ENEMY_CEILING_IN = "CeilingIn";
    public static string ENEMY_CEILING_OUT = "CeilingOut";
    public static string ENEMY_ATTACK = "Attack";
    public static string ENEMY_FALL = "Fall";
    public static string ENEMY_GROUND = "Ground";
    public static string ENEMY_SPIKE_IN = "SpikeIn";
    public static string ENEMY_SPIKE_OUT = "SpikeOut";
    public static string ENEMY_SPIKE_ON = "SpikeOn";
}
