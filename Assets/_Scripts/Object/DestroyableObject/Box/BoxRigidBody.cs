using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRigidBody : MyMonoBehaviour
{
    [SerializeField] Vector2 forceDirection;
    [SerializeField] float torque;

    Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        this.rb = GetComponent<Rigidbody2D>();

        float randTorque = Random.Range(torque - 100, torque + 100);
        float randForceX = Random.Range(forceDirection.x - 100, forceDirection.x + 300);
        float randForceY = Random.Range(forceDirection.y - 100, forceDirection.y + 300);

        forceDirection.x = randForceX;
        forceDirection.y = randForceY;

        rb.AddForce(forceDirection);
        rb.AddTorque(torque);
    }
}
