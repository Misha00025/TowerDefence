using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEnemy
{
    public UnityEvent<int> HealthChanged { get; }
    public UnityEvent<int> Died { get; }
    public int Health { get; }

    public void TakeDamage(int damage, DamageInfo info);
}
