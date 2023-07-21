using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Weapon))]
public class Tower : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Spotter _spotter;

    [SerializeField] private Transform _target;

    public EnemyGameObject Target => _target.GetComponent<EnemyGameObject>();
    public Weapon Weapon => _weapon;

    public UnityEvent<Tower> TargetUnfocus = new UnityEvent<Tower>();

    void Awake()
    {
        _weapon = GetComponent<Weapon>();
        _spotter.SetMaxDistance(_weapon.AttackRadius);
    }

    public void SetTarget(EnemyGameObject target)
    {
        this._target = target.transform;
        _spotter.Observe(target.transform);
    }

    public void TryAttack()
    {
        if (!_weapon.CanAttack(_target) || _target.gameObject == null)
        {
            TargetUnfocus.Invoke(this);
            return;
        }
        if (_weapon.IsReloaded())
        {
            Vector2 direction = _spotter.SpotDirectionFromTo(transform, _target, _weapon.LaunchSpeed);
            _weapon.Attack(direction);
        }
        _spotter.Observe(_target);
        _weapon.Reload();
    }
}
