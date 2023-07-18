using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    [SerializeField]
    private List<EnemyMover> _enemies = new List<EnemyMover>();

    private Action<Wave> _stopWave;

    public EnemyMover[] Enemies => _enemies.ToArray();

    public void Start(Action<Wave> actionOnEndOfWave)
    {
        _stopWave = actionOnEndOfWave;
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
            _stopWave(this);
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
