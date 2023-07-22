using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGameObject : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private IEnemy _currentEnemy;

    private void Awake()
    {
        Enemy.Died.AddListener((int i) => { Destroy(gameObject); });
    }

    public IEnemy Enemy 
    { 
        get { 
            if (_currentEnemy == null)
                _currentEnemy = _enemy;
            return _currentEnemy; 
        } 
        set { 
            if (value != null) _currentEnemy = value; 
        } 
    }
}
