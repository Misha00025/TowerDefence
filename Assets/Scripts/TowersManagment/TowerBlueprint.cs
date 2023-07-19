using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerBlueprint : MonoBehaviour
{
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private int _price;

    public UnityEvent<int> TowerIntansiated { get; private set; } = new UnityEvent<int>();

    public int Price => _price;

    public Tower GetInstanciatedObject()
    {
        Tower tower = Instantiate(_towerPrefab.gameObject).GetComponent<Tower>();
        TowerIntansiated.Invoke(Price);
        return tower;
    }
}
