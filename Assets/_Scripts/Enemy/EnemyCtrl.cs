using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MyMonoBehaviour
{
    [SerializeField] protected Transform model;
    public Transform Model => model;
    [SerializeField] protected EnemyDamageReceiver enemyDamageReceiver;
    public EnemyDamageReceiver EnemyDamageReceiver { get => enemyDamageReceiver; }
    [SerializeField] protected EnemyDamageSender enemyDamageSender;
    public EnemyDamageSender EnemyDamageSender { get => enemyDamageSender; }
    [SerializeField] protected EnemyAnimation enemyAnimation;
    public EnemyAnimation EnemyAnimation => enemyAnimation;
    [SerializeField] protected EnemyMovement enemyMovement;
    public EnemyMovement EnemyMovement => enemyMovement;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyAnimation();
        this.LoadModel();
        this.LoadEnemyDamageReceiver();
        this.LoadEnemyDamageSender();
        this.LoadEnemyMovement();
    }
    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<Transform>();
    }
    protected virtual void LoadEnemyDamageReceiver()
    {
        if (this.enemyDamageReceiver != null) return;
        this.enemyDamageReceiver = transform.Find("DamageReceiver").GetComponent<EnemyDamageReceiver>();
    }
    protected virtual void LoadEnemyDamageSender()
    {
        if (this.enemyDamageSender != null) return;
        this.enemyDamageSender = transform.Find("DamageSender").GetComponent<EnemyDamageSender>();
    }
    protected virtual void LoadEnemyAnimation()
    {
        if (this.enemyAnimation != null) return;
        this.enemyAnimation = transform.Find("Model").GetComponent<EnemyAnimation>();
    }
    protected virtual void LoadEnemyMovement()
    {
        if (this.enemyMovement != null) return;
        this.enemyMovement = transform.Find("Movement").GetComponent<EnemyMovement>();
    }
}
