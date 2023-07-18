using System;
using UnityEngine;

[Serializable]
public struct EnemiesInWaves
{
    [SerializeField]
    private Enemy _enemyPrefab;
    [SerializeField]
    private int _count;

    public Enemy EnemyPrefab => _enemyPrefab;
    public int Count => _count;
}
