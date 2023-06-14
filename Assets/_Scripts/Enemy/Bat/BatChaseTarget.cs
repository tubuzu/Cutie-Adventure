using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatChaseTarget : EnemyAIPathFinding
{
    bool isSleeping = true;
    bool isCeilingIn = false;
    [SerializeField] float ceilingInTime = 0.7f;
    [SerializeField] float ceilingOutTime = 0.7f;
    bool isCeilingOut = false;
    [SerializeField] protected Transform startedPosition;
    [SerializeField] protected EnemyActiveRange enemyActiveRange;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStartPosition();
        this.LoadEnemyActiveRange();
    }
    protected virtual void LoadStartPosition()
    {
        this.startedPosition = transform.parent.parent;
        this.target = startedPosition;
    }
    protected override void LoadRigidBody()
    {
        base.LoadRigidBody();
        this.rb.gravityScale = 0f;
    }
    protected virtual void LoadEnemyActiveRange()
    {
        if (this.enemyActiveRange != null) return;
        this.enemyActiveRange = transform.parent.parent.Find("ActiveRange").GetComponent<EnemyActiveRange>();
    }

    protected override void Start()
    {
        base.Start();
        this.enemyActiveRange.OnTriggerEnter += this.TargetEnterRange;
        this.enemyActiveRange.OnTriggerExit += this.TargetExitRange;
    }

    protected override void FixedUpdate()
    {
        HandleAnimation();
        base.FixedUpdate();
    }
    protected virtual void HandleAnimation()
    {
        bool wasSleeping = isSleeping;
        float distance = Vector2.Distance(rb.position, startedPosition.position);
        isSleeping = distance < 0.25f ? true : false;
        if (!isSleeping)
        {
            if (!isCeilingOut) this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_FLY);
        }
        else if (isSleeping)
        {
            if (!wasSleeping && !isCeilingIn)
            {
                this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_CEILING_IN);
                StartCoroutine(StartCeilingIn());
            }
            else if (!isCeilingIn) this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_IDLE);
        }
    }

    IEnumerator StartCeilingIn()
    {
        isCeilingIn = true;
        yield return new WaitForSeconds(ceilingInTime);
        isCeilingIn = false;
        if (IsInvoking("UpdatePath")) CancelInvoke("UpdatePath");
    }

    IEnumerator StartCeilingOut()
    {
        isCeilingOut = true;
        this.stopping = true;
        yield return new WaitForSeconds(ceilingOutTime);
        isCeilingOut = false;
        this.stopping = false;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        // Debug.Log("endCeilingOut");
    }

    public override void TargetEnterRange()
    {
        this.currentWaypoint = 0;
        this.target = PlayerCtrl.Ins.transform;
        if (isSleeping)
        {
            this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_CEILING_OUT);
            // Debug.Log("startCeilingOut");
            StartCoroutine(StartCeilingOut());
        }
    }
    public override void TargetExitRange()
    {
        this.currentWaypoint = 0;
        this.target = startedPosition;
        // if (IsInvoking("UpdatePath")) CancelInvoke("UpdatePath");
    }
}
