using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIPathFinding : EnemyMovement
{
    [SerializeField] protected Transform target;

    [SerializeField] protected float speed = 500f;
    [SerializeField] protected float nextWaypointDistance = 3f;

    protected Path path;
    protected int currentWaypoint = 0;
    // bool reachedEndOfPath = false;
    [SerializeField] protected Seeker seeker;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSeeker();
    }
    protected virtual void LoadSeeker()
    {
        if (this.seeker != null) return;
        this.seeker = transform.parent.GetComponent<Seeker>();
    }


    protected virtual void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        if (!this.stopping && !this.enemyCtrl.EnemyDamageReceiver.IsDead())
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.fixedDeltaTime;

            rb.AddForce(force);

            if ((!ShouldFacingLeft(force.x) && facingLeft) || (ShouldFacingLeft(force.x) && !facingLeft))
                this.Flip();
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            this.currentWaypoint = 0;
        }
    }

    protected virtual bool ShouldFacingLeft(float xForce)
    {
        if (xForce >= 0.01f) return false;
        else if (xForce <= -0.01f) return true;
        return facingLeft;
    }

    public virtual void TargetEnterRange()
    {

    }

    public virtual void TargetExitRange()
    {

    }
}
