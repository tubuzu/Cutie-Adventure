using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    [SerializeField] protected float disLimit = 30f;
    [SerializeField] protected float distance = 0f;
    [SerializeField] protected Camera mainCam;

    protected override void LoadComponents()
    {
        this.LoadCamera();
    }
    protected virtual void LoadCamera()
    {
        if (this.mainCam != null) return;
        this.mainCam = Transform.FindObjectOfType<Camera>();
        Debug.Log(transform.parent.name + ": LoadCamera", gameObject);
    }
    protected override bool CanDespawn()
    {
        this.distance = Vector3.Distance(transform.position, mainCam.transform.position);
        if (this.distance > disLimit) return true;
        return false;
    }
}
