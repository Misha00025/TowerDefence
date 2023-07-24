using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventSystem
{
    private static GlobalEventSystem _instance;
    public static GlobalEventSystem Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GlobalEventSystem();
            return _instance;
        }

        private set
        {

        }
    }
    public static void Clear()
    {
        _instance = null;
    }

    private GlobalEventSystem()
    {
        InputAction = new UnityEvent<Ray, IncomingAction>() { };
        LevelLoaded = new UnityEvent();
        WaveStarted = new UnityEvent<Wave>() { };
        WaveStopped = new UnityEvent<Wave>() { };
        //EnemyDamaged = new UnityEvent<EnemyGameObject, DamageInfo>() { };
        //EnemyDestroied = new UnityEvent<EnemyGameObject>() { };
    }

    public UnityEvent<Ray, IncomingAction> InputAction { get; private set; }
    public UnityEvent LevelLoaded { get; private set; }
    public UnityEvent<Wave> WaveStarted { get; private set; }
    public UnityEvent<Wave> WaveStopped { get; private set; }
    //public UnityEvent<EnemyGameObject, DamageInfo> EnemyDamaged { get; private set; }
    //public UnityEvent<EnemyGameObject> EnemyDestroied { get; private set; }

}
