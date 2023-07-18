using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private EneemyGenerator _enemyGenerator;
    [SerializeField] private Navigator _navigator;
    [SerializeField] private WavesController _wavesController;

    // Start is called before the first frame update
    public void Awake()
    {
        _gameBoard.Initialize();
        _navigator.Initialize(_gameBoard);


        _enemyGenerator.Initialize(_gameBoard);
        var waves = _enemyGenerator.GenerateWaves();

        _wavesController.Initialize(_navigator, waves);
    }
}
