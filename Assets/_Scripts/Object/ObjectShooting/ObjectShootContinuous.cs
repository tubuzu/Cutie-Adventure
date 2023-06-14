using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShootContinuous : ObjectShooting
{
    protected override void Awake()
    {
        base.Awake();
        this.canShoot = true;
    }
}
