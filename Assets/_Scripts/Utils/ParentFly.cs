using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentFly : MyMonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1;
    [SerializeField] protected Vector3 direction = Vector3.left;
    [SerializeField] protected Rigidbody2D _rb;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
    }

    protected abstract void LoadRigidbody();

    protected override void OnEnable()
    {
        // transform.parent.Translate(direction * moveSpeed * Time.deltaTime);
        this._rb.velocity = direction * moveSpeed;
    }
}
