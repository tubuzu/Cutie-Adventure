using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : Spawner
{
    private static BoxSpawner instance;
    public static BoxSpawner Instance => instance;
    public static string Box1 = "Box1";
    public static string Box1Breaks = "Box1Breaks";
    public static string Box2 = "Box2";
    public static string Box2Breaks = "Box2Breaks";
    public static string Box3 = "Box3";
    public static string Box3Breaks = "Box3Breaks";
    protected override void Awake()
    {
        base.Awake();
        if (BoxSpawner.Instance != null) Debug.LogError("Only 1 BoxSpawner allow to exist!");
        BoxSpawner.instance = this;
    }
}
