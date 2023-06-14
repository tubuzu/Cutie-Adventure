using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyAbstract
{
    protected bool facingLeft = true;
    protected bool stopping = false;
    public bool Stopping { get => stopping; set => this.stopping = value; }
    protected bool onStopPoint = false;
    public bool OnStopPoint { get => onStopPoint; }
    [SerializeField] float stopDuration = 1.5f;


    protected Rigidbody2D rb;
    [SerializeField] protected bool canFly = false;
    [SerializeField] protected float gravityScale = 3f;
    [SerializeField] protected float linearDrag = 1.5f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidBody();
    }

    protected virtual void LoadRigidBody()
    {
        if (this.rb != null) return;
        this.rb = transform.parent.GetComponent<Rigidbody2D>();
        this.rb.gravityScale = 0f;
        if (this.canFly)
        {
            this.rb.drag = linearDrag;
        }
    }

    public virtual void TurnLeft()
    {
        if (!facingLeft)
        {
            Flip();
        }
    }

    public virtual void TurnRight()
    {
        if (facingLeft)
        {
            Flip();
        }
    }

    protected void Flip()
    {
        facingLeft = !facingLeft;
        transform.parent.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDead()
    {
        this.enemyCtrl.GetComponent<Collider2D>().isTrigger = true;
        rb.gravityScale = this.gravityScale;
        rb.velocity = new Vector2(0f, 0f);
        rb.AddForce(new Vector2(0.25f, 1f) * 400);
    }

    public void Move(bool moveLeft, float xSpeed, float ySpeed)
    {
        float xVelocity = stopping ? 0 : (moveLeft ? -1 * xSpeed : xSpeed);
        this.rb.velocity = new Vector2(xVelocity, ySpeed);
        if (moveLeft && !facingLeft) Flip();
        else if (!moveLeft && facingLeft) Flip();
    }

    protected void Stop()
    {
        this.rb.velocity = new Vector2(0f, 0f);
    }

    protected IEnumerator StopForAWhile()
    {
        this.stopping = true;
        this.onStopPoint = true;
        yield return new WaitForSeconds(stopDuration);
        this.onStopPoint = false;
        this.stopping = false;
    }
}
