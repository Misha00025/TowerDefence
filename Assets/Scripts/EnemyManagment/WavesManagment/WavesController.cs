using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WavesController : MonoBehaviour
{
    [SerializeField]
    private readonly float _dely = 5;
    private IWavesGenerator _generator;
    [SerializeField]
    private List<Wave> _waves = new List<Wave>();
    private float _timer = 0;
    private bool _waveIsStarted = false;

    public float Dely => _dely;
    public bool WavesIsEmpty => _generator.WavesListIsEmpty && _waves.Count == 0;

    public UnityEvent<Wave> WaveStarted = new UnityEvent<Wave>();

    public void Initialize(IWavesGenerator enemyGenerator)
    {
        _generator = enemyGenerator;
    }

    private void Update()
    {
        if (!_waveIsStarted)
        {
            _timer += Time.deltaTime;
            if (_timer > _dely)
            {
                StartNextWave();
            }
        }
        UpdateWaves();
    }

    private void StartWave(Wave wave)
    {
        if (wave == null)
            return;
        Wave _currentWave = wave;
        _waveIsStarted= true;
        _waves.Add(_currentWave);
        _currentWave.WaveStoped.AddListener((Wave wave) =>
        {
            _waves.Remove(wave);
            if (_waves.Count == 0)
                _waveIsStarted = false;
        });
        _currentWave.Start();
        WaveStarted.Invoke(_currentWave);
    }

    private void UpdateWaves()
    {
        foreach (var wave in _waves)
        {
            wave.Update();
        }
    }

    public void StartNextWave()
    {
        if (_generator.WavesListIsEmpty) return;
        _timer = 0;
        StartWave(_generator.GetNextWave());
    }
}
