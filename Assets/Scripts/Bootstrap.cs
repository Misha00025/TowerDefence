using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameBoard _gameBoard;

    [Header("Враги и волны")]
    [SerializeField] private EnemyGenerator _enemyGenerator;
    [SerializeField] private Navigator _navigator;
    [SerializeField] private WavesController _wavesController;
    [SerializeField] private EnemySpawner _enemySpawner;

    [Header("Башни и база")]
    [SerializeField] private TowersController _towersController;
    [SerializeField] private BuildingShop _builder;
    [SerializeField] private BaseHealth _baseHealth;
    private BuilderInput _builderInput;

    [Header("Экономика")]
    [SerializeField] private PlayerWallet _playerWallet;
    private Rewarder _rewarder;

    [Header("Пользовательский ввод")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Отображение")]
    [SerializeField] private PlayerWalletView _playerWalletView;
    [SerializeField] private TextMeshProUGUI _meshPro;
    [SerializeField] private WaveTimerView _waveTimer;

    [Header("Отладка")]
    [SerializeField] private TextMeshProUGUI _debugMesh;

    // Start is called before the first frame update
    public void Start()
    {
        _gameBoard.Initialize();

        _navigator.Initialize(_gameBoard);
        _enemyGenerator.Initialize(_gameBoard);
        var waves = _enemyGenerator.GenerateWaves();
        AddListenerToFinish(waves);
        _wavesController.Initialize(waves);
        _enemySpawner.Initialize(_navigator);
        _wavesController.WaveStarted.AddListener(_enemySpawner.StartSpawnWave);

        _builder.Initialize(_gameBoard, _playerWallet);
        _towersController.Initialize();
        _wavesController.WaveStarted.AddListener(_towersController.GetReadyFor);
        _builder.NewTowerBuilded.AddListener(_towersController.AddTower);

        _rewarder = new Rewarder(waves, _playerWallet);

        InputInitialization(); 
        InitializeView();
        _playerInput.ActionActivated.AddListener((Ray ray, IncomingAction action) => { _debugMesh.SetText($"{action}"); });
    }

    private void AddListenerToFinish(List<Wave> waves)
    {
        foreach (Wave wave in waves) 
            foreach (var mover in wave.Enemies)
            {
                mover.FinishedAlive.AddListener(_baseHealth.TakeDamage);
            }
    }

    private void InputInitialization()
    {
        _playerInput.Initialize();

        _builderInput = new BuilderInput(_playerInput, _builder);
    }

    private void InitializeView()
    {
        _playerWalletView.Initialize(_playerWallet, _meshPro);
        _waveTimer.Initialize(_wavesController);
    }
}
