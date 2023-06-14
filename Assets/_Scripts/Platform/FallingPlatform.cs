using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallingPlatform : MonoBehaviour
{
    Vector3 upPos;
    [SerializeField] float fallDepth = 10f;
    [SerializeField] float fallDuration = 2f;

    [SerializeField] float timePlayerStandOn = 0.1f;
    [SerializeField] float timeBeBack = 10f;

    bool inWorkingChain = false;

    private void Awake()
    {
        this.upPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "GroundCheck" && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!inWorkingChain) StartCoroutine(StartFalling());
        }
    }

    IEnumerator StartFalling()
    {
        this.inWorkingChain = true;
        yield return new WaitForSeconds(timePlayerStandOn);
        transform.DOMoveY(upPos.y - fallDepth, fallDuration).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(timeBeBack);
        transform.DOMoveY(upPos.y, fallDuration).SetEase(Ease.OutCubic);
        this.inWorkingChain = false;
    }
}
