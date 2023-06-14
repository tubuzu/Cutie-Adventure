using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRigidbody : MyMonoBehaviour
{
    [SerializeField] Vector3 originPos;
    [SerializeField] Vector2 forceDirection;
    [SerializeField] float torque;
    [SerializeField] float torqueMutation;
    [SerializeField] float forceMutation;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float gravityScale = 3f;

    protected override void Awake()
    {
        base.Awake();
        this.originPos = transform.localPosition;
        this.rb = GetComponent<Rigidbody2D>();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidBody2D();
    }

    protected virtual void LoadRigidBody2D()
    {
        if (this.rb != null) return;
        this.rb = GetComponent<Rigidbody2D>();
        this.rb.gravityScale = this.gravityScale;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        transform.localPosition = originPos;

        float randTorque = Random.Range(torque - torqueMutation, torque + torqueMutation);
        float randForceX = Random.Range(forceDirection.x - forceMutation, forceDirection.x + forceMutation);
        float randForceY = Random.Range(forceDirection.y - forceMutation, forceDirection.y + forceMutation);

        forceDirection.x = randForceX;
        forceDirection.y = randForceY;

        rb.AddForce(forceDirection);
        rb.AddTorque(torque);
    }
}
