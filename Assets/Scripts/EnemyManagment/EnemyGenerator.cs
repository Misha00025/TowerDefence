using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] 
    private List<WavePrefab> _waves;
    private GameBoard _gameBoard;


    public void Initialize(GameBoard gameBoard)
    {
        if (_waves == null)
        {
            _waves = new List<WavePrefab>();
        }
        _gameBoard = gameBoard;
    }

    public List<Wave> GenerateWaves()
    {
        List<Wave> waves = new List<Wave>();
        foreach (var wave in _waves)
        {
            Wave newWave = new Wave();
            foreach (var enemyInWave in wave.Enemies)
                for (int i = 0; i < enemyInWave.Count; i++)
                {
                    if (enemyInWave.EnemyPrefab == null)
                        continue;
                    var enemy = Instantiate(enemyInWave.EnemyPrefab.gameObject, _gameBoard.transform);
                    newWave.AddEnemy(enemy);                    
                }
            waves.Add(newWave);
        }
            
        return waves;
    }
}
