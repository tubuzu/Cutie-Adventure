using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] bool clockwise = true;
    private void FixedUpdate()
    {
        // Vector3 nextAngle = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + Time.fixedDeltaTime * rotationSpeed);
        // transform.rotation = Quaternion.Euler(nextAngle);
        transform.Rotate(0f, 0f, (clockwise ? -1 : 1) * rotationSpeed * Time.fixedDeltaTime);
    }
}
