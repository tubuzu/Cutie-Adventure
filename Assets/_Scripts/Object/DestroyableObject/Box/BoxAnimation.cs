using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAnimation : BoxAbstract
{
    [SerializeField] private Animator animator;
    [SerializeField] private float hitDuration = 5f / 12f;

    private string currentState;
    private bool beingHit = false;

    private List<string> repeatableStates = new List<string> {
        BoxAnimationState.BOX_HIT,
    };

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
        // Debug.Log("change state" + newState);
        if (currentState == newState && !repeatableStates.Contains(newState)) return;
        animator.Play(newState.ToString(), -1, 0);
        currentState = newState;
    }

    public void OnHit()
    {
        ChangeAnimationState(BoxAnimationState.BOX_HIT);
        if (!beingHit) StartCoroutine(OnHitEnd());
    }

    IEnumerator OnHitEnd()
    {
        beingHit = true;
        yield return new WaitForSeconds(hitDuration);
        ChangeAnimationState(BoxAnimationState.BOX_IDLE);
        beingHit = false;
    }
}

public class BoxAnimationState
{
    public static string BOX_IDLE = "Idle";
    public static string BOX_HIT = "Hit";
}
