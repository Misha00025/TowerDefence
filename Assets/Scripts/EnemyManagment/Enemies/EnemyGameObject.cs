using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGameObject : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats;
    private Enemy _enemy;
    private IEnemy _currentEnemy;

    private void Awake()
    {
        _enemy = new Enemy(_stats);
        Enemy.Died.AddListener((int i) => { Destroy(gameObject); });
    }

    public EnemyStats Stats => _stats;

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
