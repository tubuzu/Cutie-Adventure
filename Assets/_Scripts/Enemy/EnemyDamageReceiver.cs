using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    public EnemyCtrl EnemyCtrl { get { return enemyCtrl; } }
    [SerializeField] float despawnDelay = 3f;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }
    protected virtual void LoadEnemyCtrl()
    {
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
        if (this.enemyCtrl == null) this.enemyCtrl = transform.GetComponent<EnemyCtrl>();
    }

    protected override void OnDead()
    {
        this.enemyCtrl.EnemyAnimation.OnHit();
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnDelay);
        Destroy(this.enemyCtrl.transform.parent.gameObject);
    }
}
