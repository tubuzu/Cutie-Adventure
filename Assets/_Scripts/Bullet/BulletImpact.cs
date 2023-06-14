using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BulletImpact : BulletAbstract
{
    [Header("Bullet Impact")]
    [SerializeField] protected CircleCollider2D bulletCollider;
    private Vector3 bulletBreaksOffset = new Vector3(0.25f, 0.25f, 0);
    const float k_ImpactRadius = .5f; // Radius of the overlap circle to determine if grounded
    [SerializeField] LayerMask impactLayer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }
    protected virtual void LoadCollider()
    {
        if (bulletCollider != null) return;
        bulletCollider = GetComponent<CircleCollider2D>();
        if (bulletCollider == null) return;
        this.bulletCollider.isTrigger = true;
        this.bulletCollider.radius = 0.05f;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.parent.position, k_ImpactRadius, impactLayer);
        if (colliders.Length <= 0) return;
        int num = colliders.Length;

        foreach (Collider2D collider in colliders)
        {
            if (!collider.gameObject.CompareTag("DamageReceiver"))
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    num--;
                continue;
            }
            this.bulletCtrl.BulletDamageSender.Send(collider.transform);
        }
        if (num <= 0) return;

        this.CreateImpactFX(colliders[0]);

        this.bulletCtrl.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

        this.bulletCtrl.BulletDespawn.DespawnObject();
    }

    protected virtual void CreateImpactFX(Collider2D collision)
    {
        string fxName = this.GetFXName();

        Vector3 collisionPoint = collision.ClosestPoint(transform.position);
        Vector3 bulletPosition = transform.position;

        // Calculate the direction vector
        Vector3 direction = bulletPosition - collisionPoint;

        // Normalize the direction vector to get a unit vector
        direction.Normalize();

        // Use LookRotation to create a quaternion based on the direction vector
        Quaternion rotation = Quaternion.LookRotation(direction);

        BulletSpawner.Instance.Spawn(fxName, transform.parent.position + bulletBreaksOffset, Quaternion.identity);
    }
    protected virtual string GetFXName()
    {
        return BulletSpawner.PlantBulletBreaks;
    }
}
