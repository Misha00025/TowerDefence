using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WavesController : MonoBehaviour
{
    [SerializeField]
    private float _dely = 5;
    [SerializeField]
    private List<Wave> _waves;
    private Wave _currentWave;

    private float _timer = 0;

    public UnityEvent<Wave> WaveStarted = new UnityEvent<Wave>();

    public void Initialize(List<Wave> waves)
    {
        _waves = waves;
        _currentWave = null;
    }

    public void StartWave(Wave wave)
    {
        _currentWave = wave;
        _currentWave.WaveStoped.AddListener((Wave wave) =>
        {
            if (wave == _currentWave)
                _currentWave = null;
            _waves.Remove(wave);
        });
        _currentWave.Start();
        WaveStarted.Invoke(_currentWave);
    }

    public void Update()
    {
        if (_currentWave == null)
        {
            if (_waves.Count > 0)
            {
                _timer += Time.deltaTime;
                if (_timer > _dely)
                {
                    _timer = 0;
                    StartWave(_waves[0]);
                }
            }
            return;
        }
        _currentWave.Update();
    }
}
