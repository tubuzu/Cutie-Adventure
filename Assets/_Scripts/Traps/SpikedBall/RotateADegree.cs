using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateADegree : MonoBehaviour
{
    private float rotationSpeed = 0f; // Tốc độ xoay
    [SerializeField] protected float baseSpeed = 1f; // Tốc độ xoay

    [SerializeField] protected float targetRotation = 60f;
    [SerializeField] protected float rotationThreshold = 1f; // Sai số cho phép
    private bool rotatingLeft = false; // Ban đầu quay sang phải

    private bool canChangeDirection = true;

    private void FixedUpdate()
    {
        float phandu = Mathf.Abs(transform.eulerAngles.z) % 360;
        float currentZRotation = Mathf.Abs(phandu > 180 ? 360 - phandu : phandu);
        if (currentZRotation > (targetRotation - rotationThreshold) && canChangeDirection)
        {
            rotatingLeft = !rotatingLeft;
            canChangeDirection = false;
        }
        if (currentZRotation < baseSpeed && !canChangeDirection)
        {
            canChangeDirection = true;
        }
        rotationSpeed = this.baseSpeed * Time.fixedDeltaTime * (this.targetRotation - currentZRotation);
        transform.Rotate(0f, 0f, (rotatingLeft ? -1 : 1) * ((rotationSpeed >= 1f) ? Mathf.Sqrt(rotationSpeed) : rotationSpeed));
    }
}
