using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyOnPath : EnemyFollowPath
{
    [SerializeField] private float yMax = 0.5f;
    [SerializeField] private float ySpeed = 4f;
    [SerializeField] private float xSpeed = 2f;
    private bool isFlyingUp = true;

    protected virtual void FixedUpdate()
    {
        if (this.enemyCtrl.EnemyDamageReceiver.IsDead()) return;
        // if (Vector2.Distance(waypoints[currentWaypointIndex].transform.parent.position, transform.parent.position) < .1f)
        if (Mathf.Abs(waypoints[currentWaypointIndex].Pos.x - transform.parent.localPosition.x) < .1f)
        {
            if (this.waypoints[currentWaypointIndex].isStopPoint) StartCoroutine(StopForAWhile());

            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
        }

        if (isFlyingUp && transform.parent.localPosition.y >= yMax)
        {
            isFlyingUp = false;
        }
        else if (!isFlyingUp && transform.parent.localPosition.y <= -yMax)
        {
            isFlyingUp = true;
        }

        if (!ShouldFacingLeft())
        {
            this.Move(false, this.stopping ? 0 : xSpeed, isFlyingUp ? ySpeed : -1 * ySpeed);
        }
        else
        {
            this.Move(true, this.stopping ? 0 : xSpeed, isFlyingUp ? ySpeed : -1 * ySpeed);
        }

        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_FLY);
    }
}
