using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSpikeInOut : EnemyAbstract
{
    [SerializeField] float spikeOnDuration = 3f;
    [SerializeField] float spikeOffDuration = 3f;
    [SerializeField] float spikeOnDelay = 8f / 18f;
    [SerializeField] float spikeOffDelay = 8f / 18f;

    // bool spikeOn = false;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(StartSpikeOn());
    }

    IEnumerator StartSpikeOn()
    {
        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_SPIKE_OUT);
        yield return new WaitForSeconds(spikeOnDelay);
        SpikeOn();
        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_SPIKE_ON);
        yield return new WaitForSeconds(spikeOnDuration);
        yield return StartSpikeOff();
    }

    IEnumerator StartSpikeOff()
    {
        SpikeOff();
        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_SPIKE_IN);
        yield return new WaitForSeconds(spikeOffDelay);
        this.enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.ENEMY_IDLE);
        yield return new WaitForSeconds(spikeOffDuration);
        yield return StartSpikeOn();
    }

    protected virtual void SpikeOff()
    {
        this.enemyCtrl.EnemyDamageSender.gameObject.SetActive(false);
        this.enemyCtrl.EnemyDamageReceiver.gameObject.SetActive(true);
    }

    protected virtual void SpikeOn()
    {
        this.enemyCtrl.EnemyDamageSender.gameObject.SetActive(true);
        this.enemyCtrl.EnemyDamageReceiver.gameObject.SetActive(false);
    }
}
