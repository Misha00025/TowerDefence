using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] private int _health = 200;

    public UnityEvent<int> HealthChanged { get; private set; } = new UnityEvent<int>();

    public int Health => _health;

    public void TakeDamage(int damage)
    {
        if (_health == 0) return;
        if (damage < 0) return;
        _health -= damage;
        HealthChanged.Invoke(Health);

    }
}
