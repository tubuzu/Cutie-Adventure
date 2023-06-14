using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunOnPath : EnemyFollowPath
{
    [SerializeField] private float xSpeed = 2f;
    protected virtual void FixedUpdate()
    {
        if (this.enemyCtrl.EnemyDamageReceiver.IsDead()) return;

        string action = stopping ? EnemyAnimationState.ENEMY_IDLE : EnemyAnimationState.ENEMY_RUN;
        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(action);

        if (stopping)
        {
            this.Stop();
            return;
        }

        if (Vector2.Distance(waypoints[currentWaypointIndex].Pos, transform.parent.localPosition) < .1f)
        {
            if (this.waypoints[currentWaypointIndex].isStopPoint) StartCoroutine(StopForAWhile());

            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
        }

        if (!ShouldFacingLeft())
        {
            this.Move(false, xSpeed, this.rb.velocity.y);
        }
        else
        {
            this.Move(true, xSpeed, this.rb.velocity.y);
        }


    }
}


