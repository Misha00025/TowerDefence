using System;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[Serializable]
[RequireComponent(typeof(Weapon))]
public class Tower : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Spotter _spotter;

    private Enemy _target;

    public Enemy Target => _target;
    public Weapon Weapon => _weapon;

    public UnityEvent<Tower> TargetUnfocus = new UnityEvent<Tower>();

    void Awake()
    {
        _weapon = GetComponent<Weapon>();
        _spotter = new Spotter(_weapon.AttackRadius);
    }

    public void SetTarget(Enemy target)
    {
        this._target = target;
        _spotter.Observe(target.transform);
    }

    public void TryAttack()
    {
        if (!_weapon.CanAttack(_target) || _target == null)
        {
            TargetUnfocus.Invoke(this);
            return;
        }
        if (_weapon.IsReloaded())
        {
            Vector2 direction = _spotter.SpotDirectionFromTo(transform, _target.transform, _weapon.LaunchSpeed);
            _weapon.Attack(direction);
        }
        _spotter.Observe(_target.transform);
        _weapon.Reload();
    }
}
