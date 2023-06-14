using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Vector3 Pos => transform.localPosition;
    [SerializeField] public bool isStopPoint = false;
}
