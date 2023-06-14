// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GroundCheck" && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.transform.parent.SetParent(transform);
            // Debug.Log("StickyPlatform enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GroundCheck" && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Debug.Log("StickyPlatform exit");
            collision.gameObject.transform.parent.SetParent(null);
        }
    }
}
