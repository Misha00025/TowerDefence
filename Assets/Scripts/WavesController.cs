using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WavesController : MonoBehaviour
{
    [SerializeField]
    private List<Wave> _waves;
    [SerializeField]
    private const float _dely = 5;
    private Wave _currentWave;

    private Navigator _navigator;

    private float _timer = 0;

    public UnityEvent<Wave> WaveStarted;

    public void Initialize(Navigator navigator, List<Wave> waves)
    {
        _navigator = navigator;
        _waves = waves;
    }

    public void StartWave(Wave wave)
    {
        _currentWave = wave;
        _currentWave.Start(_navigator);
    }

    public void Update()
    {
        if (_waves.Count > 0 && _currentWave == null)
        {
            _timer += Time.deltaTime;
            if (_timer > _dely)
            {
                _timer = 0;
                StartWave(_waves[0]);
            }
        }
        if (_currentWave == null)
            return;
        _currentWave.Update();
    }
}
