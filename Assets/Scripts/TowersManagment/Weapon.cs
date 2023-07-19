using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon
{
    [SerializeField] private Transform _turret;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackRadius = 4f;
    [SerializeField] private float _attackSpeed = 0.5f;

    private float _timeInreload = 0f;

    public void Attack(Enemy target)
    {
        if (!CanAttack(target)) return;
        if (_timeInreload > 1 / _attackSpeed)
        {
            _timeInreload = 0;
            target.TakeDamage(_damage);
        }
        Debug.DrawLine(_turret.position, target.transform.position, Color.red);
        _timeInreload += Time.deltaTime;
    }

    public bool CanAttack(Enemy target)
    {
        if (target == null) return false;
        if (_turret == null) return false;
        DrawDebugLines();
        if (_bulletPrefab == null) return false;
        bool inAttackRadius = Vector2.Distance(_turret.position, target.transform.position) < _attackRadius;
        Debug.Log($"Target {target.name} in attack radius: {inAttackRadius}");
        return inAttackRadius;
    }

    private void DrawDebugLines()
    {
        var position = _turret.position;
        Debug.DrawLine(position, position + Vector3.up * _attackRadius);
        Debug.DrawLine(position, position + Vector3.left * _attackRadius);
        Debug.DrawLine(position, position + Vector3.right * _attackRadius);
        Debug.DrawLine(position, position + Vector3.down * _attackRadius);
    }
}
