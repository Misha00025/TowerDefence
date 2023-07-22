using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WavePrefab
{
    [SerializeField]
    private List<EnemyInWave> _enemies;
    public IReadOnlyList<EnemyInWave> Enemies => _enemies;
}
