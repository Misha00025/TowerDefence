using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int _bulletsCount = 5;

    protected override void LaunchToTarget(Enemy target)
    {
        Vector3 direction3 = target.transform.position - Turret.position;
        Vector2 direction = new Vector2(direction3.x, direction3.y);
        for (int i = 0; i< _bulletsCount; i++)
        {
            LaunchToDirection(direction);
        }
    }
}
