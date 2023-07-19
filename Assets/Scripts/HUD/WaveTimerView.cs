using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimerView : MonoBehaviour
{
    [SerializeField] private Image _timerImage;
    private float _dely;
    private Coroutine _coroutine;

    public void Initialize(WavesController wavesController)
    {
        _dely = wavesController.Dely;
        wavesController.WaveStarted.AddListener(OnWaveStart);
        _coroutine = StartCoroutine(Timer());
    }

    public void OnWaveStart(Wave wave)
    {
        wave.WaveStoped.AddListener(OnWaveEnded);
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _timerImage.fillAmount = 1;
    }

    public void OnWaveEnded(Wave wave)
    {
        _coroutine = StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        while (_timerImage.fillAmount > 0)
        {
            _timerImage.fillAmount -= Time.deltaTime/_dely;
            yield return null;
        }
    }
}
