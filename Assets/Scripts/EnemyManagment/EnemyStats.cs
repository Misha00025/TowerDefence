using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Create EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private int _health = 10;
    [SerializeField] private int _reward = 10;
    [SerializeField] private float _speed = 1;

    public int Health => _health;
    public int Reward => _reward;
    public float Speed => _speed;
}
