using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Weapon))]
public class Tower : MonoBehaviour, ITower
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Enemy _target;

    public Enemy Target => _target;
    public Weapon Weapon => _weapon;

    public UnityEvent<Tower> TargetUnfocus = new UnityEvent<Tower>();

    public void SetTarget(Enemy target)
    {
        this._target = target;
    }

    public void TryAttack()
    {
        if (!_weapon.CanAttack(_target))
        {
            TargetUnfocus.Invoke(this);
            return;
        }
        Debug.Log($"Attack enemy!");
        _weapon.Attack(_target);
    }
}
