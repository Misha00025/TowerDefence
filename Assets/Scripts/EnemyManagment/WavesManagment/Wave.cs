using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Wave
{
    [SerializeField]
    private List<EnemyMover> _enemies = new List<EnemyMover>();

    public EnemyMover[] Enemies => _enemies.ToArray();

    public UnityEvent<Wave> WaveStoped = new UnityEvent<Wave>();

    public void Start()
    {

    }

    public void Update()
    {
        EnemyMover nullEnemy = null;
        foreach (var enemy in _enemies)
        {
            if (enemy == null)
                nullEnemy = enemy;
            if (enemy == null || !enemy.isActiveAndEnabled)
                continue;
            enemy.Move();
        }
        _enemies.Remove(nullEnemy);
        if (_enemies.Count == 0)
            WaveStoped.Invoke(this);
    }

    public void AddEnemy(GameObject enemy)
    {
        EnemyMover mover;
        if (enemy.TryGetComponent(out mover))
        {
            _enemies.Add(mover);
        }
        enemy.SetActive(false);
    }
}
