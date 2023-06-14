using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFall : EnemyMovement
{
    [SerializeField] protected Transform groundCheck;
    private bool grounded = true;
    const float k_GroundedRadius = .27f; // Radius of the overlap circle to determine if grounded
    [SerializeField] private LayerMask m_WhatIsGround;
    private Vector3 groundDustOffset = new Vector3(0f, -0.1f, 0f);

    private bool triggered = false;
    private bool actived = false;

    [SerializeField] protected float stuckAtGroundDuration = 2f;

    [SerializeField] protected Transform startPoint;
    [SerializeField] protected float backSpeed = 2.5f;
    [SerializeField] protected EnemyActiveRange enemyActiveRange;

    private Coroutine myCoroutine;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGroundCheck();
        this.LoadStartPoint();
        this.LoadEnemyActiveRange();
    }

    protected virtual void LoadGroundCheck()
    {
        if (this.groundCheck != null) return;
        this.groundCheck = transform.parent.Find("GroundCheck").transform;
    }

    protected virtual void LoadStartPoint()
    {
        if (this.startPoint != null) return;
        this.startPoint = transform.parent.parent.Find("StartPoint").transform;
    }
    protected virtual void LoadEnemyActiveRange()
    {
        if (this.enemyActiveRange != null) return;
        this.enemyActiveRange = transform.parent.parent.Find("ActiveRange").GetComponent<EnemyActiveRange>();
    }

    protected override void Start()
    {
        base.Start();
        this.enemyActiveRange.OnTriggerEnter += () => this.triggered = true;
        this.enemyActiveRange.OnTriggerExit += () => this.triggered = false;
    }

    protected virtual void FixedUpdate()
    {
        if (this.enemyCtrl.EnemyDamageReceiver.IsDead())
        {
            if (myCoroutine != null && myCoroutine.Equals(null) == false) // Kiểm tra xem coroutine đã được bắt đầu và đang thực thi hay không
            {
                StopCoroutine(myCoroutine); // Dừng coroutine
                myCoroutine = null; // Thiết lập lại biến coroutine thành null
            }
            return;
        }

        if (triggered && !actived) Fall();

        if (!actived) return;

        bool wasGrounded = grounded;
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, k_GroundedRadius, m_WhatIsGround);
        if (colliders.Length > 0)
        {
            grounded = true;
            if (!wasGrounded)
            {
                FXSpawner.Instance.Spawn(FXSpawner.GroundDust, groundCheck.position + groundDustOffset, Quaternion.Euler(0, 0, 0));
                this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_GROUND);
                myCoroutine = StartCoroutine(BackToStartPoint());
            }
        }
    }

    protected virtual void Fall()
    {
        this.actived = true;
        this.rb.gravityScale = this.gravityScale;
        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_FALL);
    }

    IEnumerator BackToStartPoint()
    {
        yield return new WaitForSeconds(stuckAtGroundDuration);
        if (this.enemyCtrl.EnemyDamageReceiver.IsDead()) yield break;
        this.rb.gravityScale = 0f;
        this.rb.velocity = Vector2.up * this.backSpeed;
        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_IDLE);
        yield return new WaitUntil(() => Vector2.Distance(transform.parent.position, startPoint.position) < .1f);
        if (this.enemyCtrl.EnemyDamageReceiver.IsDead()) yield break;
        this.rb.velocity = Vector2.zero;
        this.actived = false;
    }
}
