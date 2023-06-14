using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrap : MyMonoBehaviour
{
    private bool playerEntered = false;
    [SerializeField] Vector2 pullDirection = new Vector2(-1, 0);
    [SerializeField] float pullForce = 3500f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetComponent<Collider2D>().isTrigger = true;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void FixedUpdate()
    {
        if (this.playerEntered)
        {
            PlayerCtrl.Ins.PlayerMovement.AddForceToPlayer(pullDirection * pullForce * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageReceiver") && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            this.playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageReceiver") && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            this.playerEntered = false;
        }
    }
}
