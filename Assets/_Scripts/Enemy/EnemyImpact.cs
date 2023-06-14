using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpact : EnemyAbstract
{
    private float enemyDamageDelay = 1f;
    private bool triggeringPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerStatus")
        {
            if (!triggeringPlayer) triggeringPlayer = true;
            StartCoroutine(ContinuousDamage(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerStatus")
        {
            if (triggeringPlayer) triggeringPlayer = false;
        }
    }

    IEnumerator ContinuousDamage(Collider2D player)
    {
        while (triggeringPlayer)
        {
            this.enemyCtrl.EnemyDamageSender.Send(player.transform);
            yield return new WaitForSeconds(enemyDamageDelay);
        }
    }
}
