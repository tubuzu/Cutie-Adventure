using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ObjectShootByRange : ObjectShooting
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DamageReceiver") && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            this.canShoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DamageReceiver") && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            this.canShoot = false;
        }
    }
}
