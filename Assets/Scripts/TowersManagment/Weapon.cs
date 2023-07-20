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

    private List<Bullet> _bullets = new List<Bullet>();

    private float _timeInreload = 0f;

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

    public void Attack(Enemy target)
    {
        if (!CanAttack(target)) return;
        if (_timeInreload > 1 / _attackSpeed)
        {
            _timeInreload = 0;
            Bullet bullet = GetBullet();
            Vector3 dimention3 = target.transform.position - _turret.position;
            Vector2 dimention = new Vector2(dimention3.x, dimention3.y);
            bullet.StartCoroutine(bullet.PersecuteTarget(dimention, _attackRadius, _damage));
        }
        _timeInreload += Time.deltaTime;
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
