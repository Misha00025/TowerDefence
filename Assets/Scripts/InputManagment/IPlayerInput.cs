using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPlayerInput
{
    UnityEvent<Ray, IncomingAction> ActionActivated { get; }
    void CheckInput();
    bool ActionIsActive(IncomingAction action);
    Ray GetInputRay();
}
