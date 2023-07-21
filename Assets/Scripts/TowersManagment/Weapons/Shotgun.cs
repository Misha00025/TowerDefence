using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int _bulletsCount = 5;

    public override void Attack(Vector2 direction)
    {
        for (int i = 0; i< _bulletsCount; i++)
        {
            LaunchToDirection(direction);
        }
    }
}
