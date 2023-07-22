using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ArmoredEnemy : IEnemy
{
    private IEnemy _enemy;
    [SerializeField] private int _armor = 10;
    [SerializeField] private int _minDamage = 1;
    [SerializeField] private float _armoredAngle = 45;
    [SerializeField] private List<DamageType> _weaknesses;
    [SerializeField] private Transform _transform;

    public ArmoredEnemy() { }

    public ArmoredEnemy(IEnemy enemy, Transform transform)
    {
        _enemy = enemy;
        _transform = transform;
    }

    public ArmoredEnemy(IEnemy enemy, ArmoredEnemy buffPrefab)
    {
        _enemy = enemy;
        _armor = buffPrefab._armor;
        _minDamage = buffPrefab._minDamage;
        _armoredAngle = buffPrefab._armoredAngle;
        _weaknesses = buffPrefab._weaknesses;
        _transform = buffPrefab._transform;
    }

    public UnityEvent<int> HealthChanged => _enemy.HealthChanged;
    public UnityEvent<int> Died => _enemy.Died;
    public int Health => _enemy.Health;

    public void TakeDamage(int damage, DamageInfo info)
    {
        Vector2 direction = _transform.rotation * Vector2.up;
        Vector2 delta = (info.Direction - direction);

        if (!_weaknesses.Contains(info.Type))
        {
            float _angle = Mathf.Abs(Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - _transform.rotation.z);
            if (_angle > 180)
                _angle = 179;
            // Debug.Log($"Angle = {_angle}; Delta = {delta}");
            if (_angle < _armoredAngle )
                damage -= _armor;
            if (damage < _minDamage) 
                damage = _minDamage;
        }
        _enemy.TakeDamage(damage, info);
    }

}
