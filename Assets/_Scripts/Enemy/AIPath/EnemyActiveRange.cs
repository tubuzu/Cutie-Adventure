using System;
// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyActiveRange : MyMonoBehaviour
{
    public event Action OnTriggerEnter;
    public event Action OnTriggerExit;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DamageReceiver") && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DamageReceiver") && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnTriggerExit?.Invoke();
        }
    }
}
