using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Enemy : IEnemy
{
    private int _health = 10;
    private int _reward = 10;

    public Enemy(EnemyStats stats)
    {
        _health = stats.Health; 
        _reward = stats.Reward;
    }

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
