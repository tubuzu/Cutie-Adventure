using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectShooting : MyMonoBehaviour
{
    static public ObjectShooting instance;
    protected float curTime = 0f;
    [SerializeField] protected float delayTime = 1f;
    [SerializeField] protected float delayBeforeShoot = 0.1f;
    protected bool canShoot = false;
    protected bool isShooting = false;
    protected override void Awake()
    {
        ObjectShooting.instance = this;
    }
    protected virtual void FixedUpdate()
    {
        if (curTime < delayTime)
        {
            OnIdle();
            curTime += Time.fixedDeltaTime;
            return;
        }
        else if (!canShoot)
        {
            OnIdle();
        }
        else if (canShoot && !isShooting)
        {
            OnShoot();
            StartCoroutine(AboutToShoot());
        }
    }
    protected virtual void Shoot(string bulletName = "PlantBullet")
    {
        Vector3 spawnPos = transform.position;
        Quaternion rotation = transform.parent.rotation;
        Transform bullet = BulletSpawner.Instance.Spawn(bulletName, spawnPos, rotation);

        if (bullet == null) return;

        bullet.gameObject.SetActive(true);
        BulletCtrl bulletCtrl = bullet.GetComponent<BulletCtrl>();

        this.curTime = 0f;
    }

    IEnumerator AboutToShoot()
    {
        this.isShooting = true;
        yield return new WaitForSeconds(delayBeforeShoot);
        Shoot();
        this.isShooting = false;
    }

    protected virtual void OnShoot()
    {
    }
    protected virtual void OnIdle()
    {
    }
}
