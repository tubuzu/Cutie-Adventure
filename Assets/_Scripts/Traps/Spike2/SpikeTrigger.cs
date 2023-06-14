using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MyMonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject spikeTriggeredCollider;
    private bool isOn = false;
    private float timer = 0f;
    [SerializeField] float offDelay = 1.5f;
    [SerializeField] float onDelay = 2f;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
        this.LoadSpikeTriggeredCollider();
    }

    protected virtual void LoadAnimator()
    {
        if (this.anim != null) return;
        this.anim = transform.Find("Model").GetComponent<Animator>();
    }

    protected virtual void LoadSpikeTriggeredCollider()
    {
        if (this.spikeTriggeredCollider != null) return;
        this.spikeTriggeredCollider = transform.Find("SpikeOn").gameObject;
        this.spikeTriggeredCollider.SetActive(false);
    }

    protected virtual void FixedUpdate()
    {
        if (isOn)
        {
            if (timer >= onDelay)
            {
                isOn = false;
                anim.Play("Off", -1, 0);
                this.spikeTriggeredCollider.SetActive(false);
                timer = 0f;
                return;
            }
            else timer += Time.fixedDeltaTime;
        }
        else
        {
            if (timer >= offDelay)
            {
                isOn = true;
                anim.Play("On", -1, 0);
                this.spikeTriggeredCollider.SetActive(true);
                timer = 0f;
                return;
            }
            else timer += Time.fixedDeltaTime;
        }
    }
}
