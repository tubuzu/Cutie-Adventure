using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTriggerAttack : EnemyAttack
{
    // protected bool rangeTriggered = false;
    // [SerializeField] protected EnemyActiveRange enemyActiveRange;
    // protected override void LoadComponents()
    // {
    //     base.LoadComponents();
    //     this.LoadEnemyActiveRange();
    // }

    // protected virtual void LoadEnemyActiveRange()
    // {
    //     if (this.enemyActiveRange != null) return;
    //     this.enemyActiveRange = transform.parent.Find("ActiveRange").GetComponent<EnemyActiveRange>();
    // }

    [SerializeField] protected bool triggered = false;

    protected override void Awake()
    {
        base.Awake();
        this.GetComponent<Collider2D>().isTrigger = true;
    }

    protected virtual void FixedUpdate()
    {
        if (!this.isHitting && !triggered && this.enemyCtrl.EnemyMovement.Stopping && !this.enemyCtrl.EnemyMovement.OnStopPoint)
            this.enemyCtrl.EnemyMovement.Stopping = false;
            
        if (!triggered) return;

        // Debug.Log(PlayerCtrl.Ins.transform.position.x.ToString() + " " +   transform.parent.position.x.ToString());
        if (PlayerCtrl.Ins.transform.position.x < transform.parent.position.x)
            this.enemyCtrl.EnemyMovement.TurnLeft();
        else if (PlayerCtrl.Ins.transform.position.x > transform.parent.position.x)
            this.enemyCtrl.EnemyMovement.TurnRight();

        if (!this.isHitting) this.Hit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerStatus")
        {
            triggered = true;
            this.enemyCtrl.EnemyMovement.Stopping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerStatus")
        {
            triggered = false;
        }
    }
}

