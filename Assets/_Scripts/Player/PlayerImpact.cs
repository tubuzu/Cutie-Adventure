using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpact : PlayerAbstract
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DamageReceiver"))
        {
            this.playerCtrl.PlayerDamageSender.Send(other.transform);
        }
    }
}
