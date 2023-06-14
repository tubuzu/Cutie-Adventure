using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGate : MyMonoBehaviour
{
    [SerializeField] private GameObject damSenderCollider;
    [SerializeField] private LightningAnimation anim;

    // [SerializeField] private float curTime = 0f;
    [SerializeField] private float delayTime = 1f;

    // private bool isOn = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
        this.LoadDamSenderCollider();
    }
    protected virtual void LoadAnimator()
    {
        if (this.anim != null) return;
        this.anim = transform.Find("Model").GetComponent<LightningAnimation>();

    }
    protected virtual void LoadDamSenderCollider()
    {
        if (this.damSenderCollider != null) return;
        this.damSenderCollider = transform.Find("DamageSender").gameObject;
    }

    protected override void Start()
    {
        base.Start();
        this.anim.OnLightningEnd += OnLightningEnd;
        StartCoroutine(StartLightning());
    }

    IEnumerator StartLightning()
    {
        yield return new WaitForSeconds(delayTime);
        this.anim.animator.Play("On");
        this.damSenderCollider.SetActive(true);
    }

    public virtual void OnLightningEnd()
    {
        // this.isOn = false;
        this.anim.animator.Play("Off");
        this.damSenderCollider.SetActive(false);
        StartCoroutine(StartLightning());
    }
}
