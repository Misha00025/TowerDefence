using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemy : Enemy
{
    [SerializeField] public int _armor = 10;
    [SerializeField] public float _armoredAngle = 45;
    [SerializeField] public List<DamageType> _weaknesses;
    public override void TakeDamage(int damage, DamageInfo info)
    {
        Vector2 direction = transform.rotation * Vector2.up;
        Vector2 delta = (info.Direction - direction);

        if (!_weaknesses.Contains(info.Type))
        {
            float _angle = Mathf.Abs(Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - transform.rotation.z);
            if (_angle > 180)
                _angle = 179;
            // Debug.Log($"Angle = {_angle}; Delta = {delta}");
            if (_angle < _armoredAngle )
                damage -= _armor;
        }
        base.TakeDamage(damage, info);
    }

}
