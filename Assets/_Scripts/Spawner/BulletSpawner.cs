using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance { get => instance; }
    public static string PlantBullet = "PlantBullet";
    public static string PlantBulletBreaks = "PlantBulletBreaks";
    protected override void Awake()
    {
        base.Awake();
        if (BulletSpawner.Instance != null) Debug.LogError("Only 1 BulletSpawner allow to exist!");
        BulletSpawner.instance = this;
    }
}
