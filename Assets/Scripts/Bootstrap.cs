using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private EnemyGenerator _enemyGenerator;
    [SerializeField] private Navigator _navigator;
    [SerializeField] private WavesController _wavesController;
    [SerializeField] private EnemySpawner _enemySpawner;

    // Start is called before the first frame update
    public void Awake()
    {
        _gameBoard.Initialize();
        _navigator.Initialize(_gameBoard);


        _enemyGenerator.Initialize(_gameBoard);
        var waves = _enemyGenerator.GenerateWaves();

        _wavesController.Initialize(waves);
        _enemySpawner.Initialize(_navigator);
        _wavesController.WaveStarted.AddListener(_enemySpawner.StartSpawnWave);
    }
}
