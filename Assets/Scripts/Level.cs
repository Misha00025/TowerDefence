using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private BaseHealth baseHealth;
    [SerializeField] private WavesController _wavesController;
    [SerializeField] private MenuTogger _winMenu;
    [SerializeField] private MenuTogger _loseMenu;

    private MenusManager _menusManager;

    public void Initialize(MenusManager menusManager)
    {
        _menusManager = menusManager;
    }

    private void Update()
    {
        
    }

    public void OnWaveStart(Wave wave)
    {
        wave.WaveStoped.AddListener(OnWaveEnd);
    }

    public void OnWaveEnd(Wave wave)
    {
        if (_wavesController.WavesIsEmpty) Win();
    }

    public void OnBaseHealthChanged(int health)
    {
        if (health <= 0) Lose();
    }

    public void Win()
    {
        _menusManager.OpenMenu(_winMenu);
    }

    public void Lose()
    {
        _menusManager.OpenMenu(_loseMenu);
    }
}
