using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;

public class PlayerStatus : DamageReceiver
{
    protected Rigidbody2D rb;
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl { get { return playerCtrl; } }

    private int trapCount = 0;
    public bool isOnTrap => trapCount > 0;
    private float trapDamageDelay = 1.5f;
    private float trapDamageTiming = 0f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }
    protected virtual void LoadPlayerCtrl()
    {
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
    }

    protected override void Start()
    {
        base.Start();
        rb = this.playerCtrl.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            trapCount++;
        }
    }

    private void FixedUpdate()
    {
        if (trapDamageTiming < trapDamageDelay)
            trapDamageTiming += Time.fixedDeltaTime;
        if (trapCount > 0 && trapDamageTiming >= trapDamageDelay)
        {
            trapDamageTiming = 0;
            this.Deduct(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            trapCount--;
        }
    }

    IEnumerator Die()
    {
        isDead = true;

        playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_DISAPPEARING);
        rb.bodyType = RigidbodyType2D.Static;

        AudioManager.Ins.PlaySFX(EffectSound.LoseSound);

        yield return new WaitUntil(() => isDead == false);

        GameManager.Ins.RestartLevel();
    }

    public void OnDeadEnd()
    {
        this.isDead = false;
    }

    protected override void OnDead()
    {
        StartCoroutine(Die());
    }

    protected override void OnDeduct()
    {
        this.playerCtrl.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_HIT);
        LifeCount.Ins.LoseLife();
        // this.playerCtrl.PlayerMovement.OnTrap();
        AudioManager.Ins.PlaySFX(EffectSound.HitSound);
        CameraShaker.Invoke();
    }

    protected override void OnAdd()
    {
        LifeCount.Ins.AddLife();
    }
}
