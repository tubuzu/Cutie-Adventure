using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ObjectAIPathFinding : MyMonoBehaviour
{
    [SerializeField] protected AIPath aiPath;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAIPath();
    }

    protected virtual void LoadAIPath()
    {
        if (aiPath != null) return;
        this.aiPath = transform.parent.GetComponent<AIPath>();
    }

    private void Update() {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            
        }    
    }
}
