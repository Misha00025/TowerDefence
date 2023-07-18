using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    [SerializeField]
    private List<EnemyMover> _enemies = new List<EnemyMover>();
    [SerializeField]
    private Queue<GameObject> _unspawnedEnemies = new Queue<GameObject>();
    private Vector2[] _route;
    private float _timer = 0;
    private const float _dely = 1;

    public void Start(Navigator navigator)
    {
        _route = navigator.Route;
        SpawnNewEnemy();
    }

    public void Update()
    {
        TrySpawnNewEnemy();
        foreach (var enemy in _enemies)
        {
            if (enemy == null)
                continue;
            enemy.Move();
        }
    }

    private void TrySpawnNewEnemy()
    {
        if (_unspawnedEnemies.Count > 0)
        {
            _timer += Time.deltaTime;
            if (_timer > _dely)
            {
                _timer = 0;
                SpawnNewEnemy();
            }
        }
    }

    private void SpawnNewEnemy()
    {
        var newEnemy = _unspawnedEnemies.Dequeue();
        EnemyMover mover;
        if (newEnemy.TryGetComponent<EnemyMover>(out mover))
        {
            mover.SetRoute(_route);
            _enemies.Add(mover);
            newEnemy.transform.position = _route[0];
            newEnemy.SetActive(true);
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        _unspawnedEnemies.Enqueue(enemy);
        enemy.SetActive(false);
    }
}
