using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestBoot : MonoBehaviour
{
    [SerializeField] private UnityEvent bootEvents = new UnityEvent();

    private void Awake()
    {
        Invoke();
    }

    [ContextMenu("Рассчитать")]
    public void Invoke()
    {

        bootEvents.Invoke();
    }
}
