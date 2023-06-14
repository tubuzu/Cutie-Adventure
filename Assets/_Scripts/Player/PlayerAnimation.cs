using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : PlayerAbstract
{
    public List<RuntimeAnimatorController> controllers;
    private int characterSelected;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private string currentState;

    public bool beingHit = false;

    [SerializeField] float appearingDuration = 1f / 3f;
    [SerializeField] float hitDuration = 6f / 20f;
    [SerializeField] float doubleJumpDuration = 1f / 3f;
    [SerializeField] float deadDuration = 1f / 3f;

    [SerializeField] float hitOpacity = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        this.animator = transform.GetComponent<Animator>();
        this.characterSelected = PlayerPrefs.GetInt("SelectedCharacter", 0);
        this.animator.runtimeAnimatorController = controllers[characterSelected];
        transform.GetComponent<SpriteRenderer>().sprite = null;
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

    protected virtual void LoadSpriteRenderer()
    {
        if (spriteRenderer != null) return;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == PlayerAnimationState.PLAYER_DISAPPEARING) return;
        if (currentState == newState && newState != PlayerAnimationState.PLAYER_HIT) return;

        animator.Play(newState.ToString(), -1, 0);

        currentState = newState;

        if (newState == PlayerAnimationState.PLAYER_APPEARING)
        {
            StartCoroutine(OnAppearing());
        }
        else if (newState == PlayerAnimationState.PLAYER_HIT)
        {
            StartCoroutine(OnHitAnimation());
        }
        else if (newState == PlayerAnimationState.PLAYER_DOUBLE_JUMP)
        {
            StartCoroutine(OnDoubleJump());
        }
        else if (newState == PlayerAnimationState.PLAYER_DISAPPEARING)
        {
            StartCoroutine(OnDead());
        }
    }

    IEnumerator OnAppearing()
    {
        yield return new WaitForSeconds(appearingDuration);
        PlayerCtrl.Ins.PlayerMovement.ApplyGravity();
    }

    IEnumerator OnHitAnimation()
    {
        this.beingHit = true;
        yield return new WaitForSeconds(hitDuration);
        this.beingHit = false;
        yield return ChangeOpacity(hitOpacity, 0.2f);
        yield return ChangeOpacity(1f, 0.2f);
        yield return ChangeOpacity(hitOpacity, 0.2f);
        yield return ChangeOpacity(1f, 0.2f);
        yield return ChangeOpacity(hitOpacity, 0.2f);
        yield return ChangeOpacity(1f, 0.2f);
    }

    IEnumerator OnDoubleJump()
    {
        yield return new WaitForSeconds(doubleJumpDuration);
        PlayerCtrl.Ins.PlayerMovement.OnDoubleJumpEnd();
    }

    IEnumerator OnDead()
    {
        yield return new WaitForSeconds(deadDuration);
        PlayerCtrl.Ins.PlayerStatus.OnDeadEnd();
    }

    private IEnumerator ChangeOpacity(float targetOpacity, float duration)
    {
        float currentOpacity = spriteRenderer.color.a;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            float newOpacity = Mathf.Lerp(currentOpacity, targetOpacity, t);
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = newOpacity;
            spriteRenderer.color = spriteColor;
            yield return null;
        }
    }
}

public class PlayerAnimationState
{
    public static string PLAYER_IDLE = "Idle";
    public static string PLAYER_RUN = "Run";
    public static string PLAYER_JUMP = "Jump";
    public static string PLAYER_DOUBLE_JUMP = "DoubleJump";
    public static string PLAYER_APPEARING = "Appearing";
    public static string PLAYER_DISAPPEARING = "Disappearing";
    public static string PLAYER_HIT = "Hit";
    public static string PLAYER_FALL = "Fall";
    public static string PLAYER_WALL_JUMP = "WallJump";
}
