using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputActions : MonoBehaviour
{
    [SerializeField] private UnityEvent ReturnActions = new UnityEvent();
    [SerializeField] private UnityEvent ExitActions = new UnityEvent();

    public void Initialize(PlayerInput playerInput)
    {
        playerInput.ActionActivated.AddListener(OnPlayerInput);
    }

    private void OnPlayerInput(Ray ray, IncomingAction action)
    {
        switch (action)
        {
            case IncomingAction.None:
                break;
            case IncomingAction.Major:
                break;
            case IncomingAction.Minor:
                break;
            case IncomingAction.Drag:
                break;
            case IncomingAction.Drop:
                break;
            case IncomingAction.Return:
                ReturnActions.Invoke();
                break;
            case IncomingAction.Exit:
                ExitActions.Invoke();
                break;
        }
    }
}
