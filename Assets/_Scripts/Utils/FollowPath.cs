using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MyMonoBehaviour
{
    [SerializeField] protected List<WayPoint> waypoints;
    protected int currentWaypointIndex = 0;

    protected bool stopping = false;
    [SerializeField] protected float stopDuration = 1.5f;
    [SerializeField] protected float speed = 4f;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaypoints();
    }
    protected virtual void LoadWaypoints()
    {
        this.waypoints = new List<WayPoint>();
        Transform wps = transform.parent.Find("Waypoints");
        for (int i = 0; i < wps.childCount; i++)
        {
            GameObject child = wps.GetChild(i).gameObject;
            if (child != null && child.activeSelf == true) this.waypoints.Add(child.GetComponent<WayPoint>());
        }
    }

    protected virtual void FixedUpdate()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            if (this.waypoints[currentWaypointIndex].isStopPoint) StopForAWhile();
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
        }
        if (!stopping)
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.fixedDeltaTime * speed);
    }

    protected virtual void StopForAWhile()
    {
        StartCoroutine(StopForADuration());
    }

    protected virtual IEnumerator StopForADuration()
    {
        this.stopping = true;
        yield return new WaitForSeconds(stopDuration);
        this.stopping = false;
    }
}
