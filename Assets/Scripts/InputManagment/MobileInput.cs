using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MobileInput : IPlayerInput
{
    private float _delayToDrag;
    private float _delay = 0;
    private bool _isDragging = false;

    private Ray _inputRay;

    public UnityEvent<Ray, IncomingAction> ActionActivated { get; private set; } = new UnityEvent<Ray, IncomingAction>();

    public MobileInput(float dragToDelay)
    {
        _delayToDrag = dragToDelay;
    }

    public bool ActionIsActive(IncomingAction action)
    {
        throw new System.NotImplementedException();
    }

    public void CheckInput()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    SaveRay(touch);
                    break;
                case TouchPhase.Moved:
                    SaveRay(touch); 
                    if (!_isDragging)
                    {
                        _isDragging = true;
                        _delay = _delayToDrag;
                        ActionActivated.Invoke(_inputRay, IncomingAction.Drag);
                    }
                    break;
                case TouchPhase.Stationary:
                    if (_delay < _delayToDrag)
                    {
                        _delay += Time.deltaTime;
                    }
                    else if (!_isDragging)
                    {
                        _isDragging = true;
                        ActionActivated.Invoke(_inputRay, IncomingAction.Drag);
                    }
                    break;
                case TouchPhase.Ended:
                    if (!_isDragging)
                        ActionActivated.Invoke(_inputRay, IncomingAction.Major);
                    if (_isDragging)
                        ActionActivated.Invoke(_inputRay, IncomingAction.Drop);
                    _isDragging = false;
                    _delay = 0;
                    break;
                case TouchPhase.Canceled:
                    break;
            }
            break;
        }
    }

    public Ray GetInputRay()
    {
        return _inputRay;
    }

    private void SaveRay(Touch touch)
    {
        _inputRay = Camera.main.ScreenPointToRay(touch.position);
    }
}
