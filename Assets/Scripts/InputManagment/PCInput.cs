using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PCInput : IPlayerInput
{
    private float _delayToDrag;
    private float _delay = 0;
    private bool _isDragging = false;

    private Ray _inputRay;

    public UnityEvent<Ray, IncomingAction> ActionActivated { get; private set; } = new UnityEvent<Ray, IncomingAction>();

    public PCInput(float dragToDelay)
    {
        _delayToDrag = dragToDelay;
    }

    public bool ActionIsActive(IncomingAction action)
    {
        throw new System.NotImplementedException();
    }

    public void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SaveRay(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && _inputRay.origin != Camera.main.ScreenPointToRay(Input.mousePosition).origin)
        {
            SaveRay(Input.mousePosition);
            if (!_isDragging)
            {
                _isDragging = true;
                _delay = _delayToDrag;
                ActionActivated.Invoke(_inputRay, IncomingAction.Drag);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (_delay < _delayToDrag)
            {
                _delay += Time.deltaTime;
            }
            else if (!_isDragging)
            {
                _isDragging = true;
                ActionActivated.Invoke(_inputRay, IncomingAction.Drag);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (!_isDragging)
                ActionActivated.Invoke(_inputRay, IncomingAction.Major);
            if (_isDragging)
                ActionActivated.Invoke(_inputRay, IncomingAction.Drop);
            _isDragging = false;
            _delay = 0;
        }
    }

    public Ray GetInputRay()
    {
        return _inputRay;
    }

    private void SaveRay(Vector2 position)
    {
        _inputRay = Camera.main.ScreenPointToRay(position);
    }
}
