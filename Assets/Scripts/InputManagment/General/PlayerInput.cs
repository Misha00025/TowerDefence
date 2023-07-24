using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private float _delayToDrag = 0.3f;

    private IPlayerInput _playerInput;

    public void Initialize()
    {
        Debug.Log(Application.platform.ToString());
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor: 
                _playerInput = new PCInput(_delayToDrag); 
                break;
            case RuntimePlatform.WindowsPlayer:
                _playerInput = new PCInput(_delayToDrag);
                break;
            case RuntimePlatform.IPhonePlayer:
                _playerInput = new MobileInput(_delayToDrag);
                break;
            case RuntimePlatform.Android:
                _playerInput = new MobileInput(_delayToDrag);
                break;
        }
    }

    public UnityEvent<Ray, IncomingAction> ActionActivated => _playerInput.ActionActivated;

    public bool ActionIsActive(IncomingAction action)
    {
        if (_playerInput == null) return false;
        return _playerInput.ActionIsActive(action);
    }

    public void CheckInput()
    {
        if (_playerInput == null) throw new Exception("Нету инпута, всё фигня!");
        _playerInput.CheckInput();
    }

    private void Update()
    {
        CheckInput();
    }

    public Ray GetInputRay()
    {
        if (_playerInput == null) return new Ray();
        return _playerInput.GetInputRay();
    }
}
