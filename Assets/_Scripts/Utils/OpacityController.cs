using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class OpacityController : MyMonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float opacityWhenTrigger = 0.3f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpriteRenderer();
        this.GetComponent<Collider2D>().isTrigger = true;
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.spriteRenderer != null) return;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DamageReceiver") && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(ChangeOpacity(opacityWhenTrigger, 0.2f));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DamageReceiver") && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(ChangeOpacity(1f, 0.2f));
        }
    }

    private IEnumerator ChangeOpacity(float targetOpacity, float duration)
    {
        float currentOpacity = spriteRenderer.color.a;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            float newOpacity = Mathf.Lerp(currentOpacity, targetOpacity, t);
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = newOpacity;
            spriteRenderer.color = spriteColor;
            yield return null;
        }
    }
}
