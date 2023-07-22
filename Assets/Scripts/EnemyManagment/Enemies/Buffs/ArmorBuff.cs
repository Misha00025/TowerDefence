using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyGameObject))]
public class ArmorBuff : MonoBehaviour
{
    [SerializeField] private ArmoredEnemy _buff;

    private void Awake()
    {
        EnemyGameObject enemyGameObject = GetComponent<EnemyGameObject>();
        IEnemy enemy = enemyGameObject.Enemy;
        enemyGameObject.Enemy = new ArmoredEnemy(enemy, _buff);
    }
}
