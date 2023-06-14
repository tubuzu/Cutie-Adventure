using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MyMonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject fireCollider;
    private bool playerInteracting = false;
    private bool isFireOn = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
        this.LoadFire();
    }

    protected virtual void LoadAnimator()
    {
        if (this.anim != null) return;
        this.anim = transform.Find("Model").GetComponent<Animator>();
    }

    protected virtual void LoadFire()
    {
        if (this.fireCollider != null) return;
        this.fireCollider = transform.Find("Fire").gameObject;
        this.fireCollider.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "GroundCheck" && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!this.playerInteracting) this.playerInteracting = true;
            if (!isFireOn)
            {
                this.anim.Play("Hit");
                StartCoroutine(TriggerFire());
            }
        }
    }

    IEnumerator TriggerFire()
    {
        yield return new WaitForSeconds(1f);
        this.fireCollider.SetActive(true);
        this.isFireOn = true;
        this.anim.Play("On");

        if (playerInteracting)
        {
            yield return new WaitUntil(() => this.playerInteracting == false);
        }

        yield return new WaitForSeconds(1f);
        this.isFireOn = false;
        this.anim.Play("Off");
        this.fireCollider.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "GroundCheck" && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (this.playerInteracting) this.playerInteracting = false;
        }
    }
}
