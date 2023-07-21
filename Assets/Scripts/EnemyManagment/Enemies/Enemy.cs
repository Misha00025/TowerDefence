using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Enemy : IEnemy
{
    [SerializeField] private int _health = 10;
    [SerializeField] private int _reward = 10;

    public UnityEvent<int> HealthChanged { get; private set; } = new UnityEvent<int>();
    public UnityEvent<int> Died { get; private set; } = new UnityEvent<int>();
    public int Health => _health;

    public void TakeDamage(int damage, DamageInfo info)
    {
        if (_health == 0) return;
        if (damage < 0) return;

        _health -= damage;
        HealthChanged.Invoke(Health);

        if (Health <= 0)
        {
            Died.Invoke(_reward);
            //Destroy(gameObject);
        }
    }
}
