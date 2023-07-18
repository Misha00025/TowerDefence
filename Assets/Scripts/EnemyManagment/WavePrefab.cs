using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WavePrefab
{
    [SerializeField]
    private List<EnemiesInWaves> _enemies;
    public IReadOnlyList<EnemiesInWaves> Enemies => _enemies;
}
