using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : Spawner
{
    private static FXSpawner instance;
    public static FXSpawner Instance => instance;
    public static string CollectedEffect = "CollectedEffect";
    public static string WallDust = "WallDust";
    public static string GroundDust = "GroundDust";
    public static string BoxExplode = "BoxExplode";
    protected override void Awake()
    {
        base.Awake();
        if (FXSpawner.Instance != null) Debug.LogError("Only 1 FXSpawner allow to exist!");
        FXSpawner.instance = this;
    }
}
