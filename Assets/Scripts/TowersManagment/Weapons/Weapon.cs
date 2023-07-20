using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _turret;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackRadius = 4f;
    [SerializeField] private float _attackSpeed = 0.5f;
    [SerializeField] private float _spread = 10;
    private System.Random random = new System.Random();

    private List<Bullet> _bullets = new List<Bullet>();

    private float _timeInreload = 0f;

    public Transform Turret => _turret;

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
        bullet.StartCoroutine(bullet.PersecuteTarget((direction + offset).normalized, _attackRadius, _damage));
        _timeInreload = 0;
    }

    protected virtual void LaunchToTarget(Enemy target)
    {
        Vector3 direction3 = target.transform.position - _turret.position;
        Vector2 direction = new Vector2(direction3.x, direction3.y);
        LaunchToDirection(direction);
    }

    public virtual void Attack(Enemy target)
    {
        if (!CanAttack(target)) return;
        if (IsReloaded())
        {
            LaunchToTarget(target);             
        }
        Reload();
    }

    public bool CanAttack(Enemy target)
    {
        if (target == null) return false;
        if (_turret == null) return false;
        if (_bulletPrefab == null) return false;
        bool inAttackRadius = Vector2.Distance(_turret.position, target.transform.position) < _attackRadius;
        return inAttackRadius;
    }
}
