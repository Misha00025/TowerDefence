using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private Level _level;

    [Header("����� � �����")]
    [SerializeField] private WavesGenerator _enemyGenerator;
    [SerializeField] private Navigator _navigator;
    [SerializeField] private WavesController _wavesController;
    [SerializeField] private EnemySpawner _enemySpawner;

    [Header("����� � ����")]
    [SerializeField] private TowersController _towersController;
    [SerializeField] private BuildingShop _builder;
    [SerializeField] private BaseHealth _baseHealth;
    private BuilderInput _builderInput;

    [Header("���������")]
    [SerializeField] private PlayerWallet _playerWallet;
    private Rewarder _rewarder;

    [Header("���������������� ����")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private InputActions _inputActions;

    [Header("�����������")]
    [SerializeField] private PlayerWalletView _playerWalletView;
    [SerializeField] private TextMeshProUGUI _meshPro;
    [SerializeField] private WaveTimerView _waveTimer;

    [Header("����")]
    [SerializeField] private MenusManager _menusManager;
    [SerializeField] private MenuTogger _menu;
    [SerializeField] private MenuTogger _winMenu;
    [SerializeField] private MenuTogger _loseMenu;

    [Header("�������")]
    [SerializeField] private TextMeshProUGUI _debugMesh;

    // Start is called before the first frame update
    public void Awake()
    {
        _gameBoard.Initialize();
        _level.Initialize(_menusManager);

        _navigator.Initialize(_gameBoard);
        _enemyGenerator.Initialize(_gameBoard);
        _wavesController.Initialize(_enemyGenerator);
        _enemySpawner.Initialize(_navigator);

        _builder.Initialize(_gameBoard, _playerWallet);
        _towersController.Initialize();

        _rewarder = new Rewarder(_playerWallet);

        InputInitialization(); 
        InitializeView();

        AddListeners();
    }

    private void AddListeners()
    {
        _playerInput.ActionActivated.AddListener((Ray ray, IncomingAction action) => { _debugMesh.SetText($"{action}"); });

        _wavesController.WaveStarted.AddListener(_enemySpawner.StartSpawnWave);
        _wavesController.WaveStarted.AddListener(_towersController.GetReadyFor);
        _wavesController.WaveStarted.AddListener(_baseHealth.OnWaveStart);
        _wavesController.WaveStarted.AddListener(_rewarder.OnWaveStart);
        _wavesController.WaveStarted.AddListener(_level.OnWaveStart);

        _baseHealth.HealthChanged.AddListener(_level.OnBaseHealthChanged);

        _builder.NewTowerBuilded.AddListener(_towersController.AddTower);
    }

    private void InputInitialization()
    {
        _playerInput.Initialize();
        _inputActions.Initialize(_playerInput);
        _builderInput = new BuilderInput(_playerInput, _builder);
    }

    private void InitializeView()
    {
        _playerWalletView.Initialize(_playerWallet, _meshPro);
        _waveTimer.Initialize(_wavesController);

        _menu.Initialize(_menusManager);
        _loseMenu.Initialize(_menusManager);
        _winMenu.Initialize(_menusManager);
    }
}
