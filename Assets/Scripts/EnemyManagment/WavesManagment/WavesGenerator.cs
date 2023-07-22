using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesGenerator : MonoBehaviour, IWavesGenerator
{
    [SerializeField] 
    private List<WavePrefab> _wavesPrefabs;
    private List<Wave> _waves = new List<Wave>();
    private GameBoard _gameBoard ;

    public bool WavesListIsEmpty => _waves.Count == 0;

    public void Initialize(GameBoard gameBoard)
    {
        if (_wavesPrefabs == null)
            _wavesPrefabs = new List<WavePrefab>();
        _gameBoard = gameBoard;
        StartCoroutine(AsyncGenerateWaves());
    }

    private IEnumerator AsyncGenerateWaves()
    {
        yield return null;
        foreach (var wave in _wavesPrefabs)
        {
            Wave newWave = GenerateWave(wave);
            _waves.Add(newWave);
            yield return null;
        }
        _wavesPrefabs.Clear();
    }

    private void GenerateWaves()
    {
        foreach (var wave in _wavesPrefabs)
        {
            Wave newWave = GenerateWave(wave);
            _waves.Add(newWave);
        }
        _wavesPrefabs.Clear();
    }

    public Wave GenerateWave(WavePrefab wave)
    {
        Wave newWave = new Wave();
        foreach (EnemyInWave enemyInWave in wave.Enemies)
            for (int i = 0; i < enemyInWave.Count; i++)
            {
                if (enemyInWave.EnemyPrefab == null)
                    continue;
                var enemy = Instantiate(enemyInWave.EnemyPrefab.gameObject, _gameBoard.transform);
                newWave.AddEnemy(enemy);
            }
        return newWave;
    }

    public Wave GetNextWave()
    {
        if (WavesListIsEmpty && _wavesPrefabs.Count > 0)
            GenerateWaves();
        if (WavesListIsEmpty)
            return null;
        Wave nexWave = _waves[0];
        _waves.RemoveAt(0);
        return nexWave;
    }
}
