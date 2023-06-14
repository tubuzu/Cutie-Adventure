using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPath : EnemyMovement
{
    [SerializeField] protected List<EnemyWayPoint> waypoints;
    protected int currentWaypointIndex = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaypoints();
    }
    protected virtual void LoadWaypoints()
    {
        this.waypoints = new List<EnemyWayPoint>();
        Transform wps = transform.parent.parent.Find("Waypoints");
        for (int i = 0; i < wps.childCount; i++)
        {
            GameObject child = wps.GetChild(i).gameObject;
            if (child != null && child.activeSelf == true) this.waypoints.Add(child.GetComponent<EnemyWayPoint>());
        }
    }

    protected virtual bool ShouldFacingLeft()
    {
        // int prevWayPoint = (currentWaypointIndex - 1) >= 0 ? currentWaypointIndex - 1 : waypoints.Count - 1;
        if (waypoints[currentWaypointIndex].Pos.x - transform.parent.localPosition.x > 0) return false;
        return true;
    }
}
