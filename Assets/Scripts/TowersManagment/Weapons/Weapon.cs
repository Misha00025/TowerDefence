using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _turret;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackRadius = 4f;
    [SerializeField] private float _attackSpeed = 0.5f;
    [SerializeField] private float _launchSpeed = 1f;
    [SerializeField] private float _spread = 10;


    private System.Random random = new System.Random();

    private List<Bullet> _bullets = new List<Bullet>();

    private float _timeInreload = 0f;

    public Transform Turret => _turret;
    public float AttackRadius => _attackRadius;
    public float LaunchSpeed => _launchSpeed;

    public void Reload()
    {
        _timeInreload += Time.deltaTime;
    }

    public bool IsReloaded()
    {
        bool isReloaded = _timeInreload > 1 / _attackSpeed;
        return isReloaded;
    }

    private Bullet GetBullet()
    {
        Bullet reservedBullet = null;
        foreach (var bullet in _bullets)
        {
            if (bullet.isActiveAndEnabled) continue;
            reservedBullet = bullet;
        }
        if (reservedBullet == null)
        {
            reservedBullet = GameObject.Instantiate(_bulletPrefab).GetComponent<Bullet>();
            _bullets.Add(reservedBullet);
        }
        reservedBullet.gameObject.SetActive(true);
        reservedBullet.transform.position = _turret.position;
        return reservedBullet;
    }

    public void LaunchToDirection(Vector2 direction)
    {
        float angle = (float)(random.NextDouble() * _spread * 2) - _spread;
        Vector2 offset = Quaternion.Euler(0, 0, angle) * direction;
        Bullet bullet = GetBullet();
        bullet.LaunchTo((direction + offset).normalized, _attackRadius, _damage, _launchSpeed);
        _timeInreload = 0;
    }

    public virtual void Attack(Vector2 direction)
    {
        LaunchToDirection(direction);
    }

    public bool CanAttack(Transform target)
    {
        if (target == null) return false;
        if (_turret == null) return false;
        if (_bulletPrefab == null) return false;
        bool inAttackRadius = Vector2.Distance(_turret.position, target.position) < _attackRadius;
        return inAttackRadius;
    }
}
