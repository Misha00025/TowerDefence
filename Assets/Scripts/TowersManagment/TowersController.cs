using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowersController : MonoBehaviour
{
    [SerializeField] private List<Tower> _towers = new List<Tower>();
    [SerializeField] private List<EnemyGameObject> _enemiesInWave = new List<EnemyGameObject>();

    private void Update()
    {
        if (_enemiesInWave == null) return;
        //Debug.Log($"Current enemies count: {_enemiesInWave.Count}");
        if (_enemiesInWave.Count == 0) return;

        foreach (var tower in _towers)
        {
            //Debug.Log($"TryAttack");
            tower.TryAttack();
        }
    }

    public void Initialize()
    {
        foreach(var tower in _towers)
        {
            tower.TargetUnfocus.AddListener(FindTargetTo);
        }
    }

    public void GetReadyFor(Wave wave)
    {        
        var list = wave.Enemies;
        foreach (var enemy in list)
        {
            //Debug.Log($"{enemy} added to enemy list");
            _enemiesInWave.Add(enemy.GameObject);
        }
    }

    public void FindTargetTo(Tower tower)
    {
        foreach (var target in _enemiesInWave)
        {
            if (target == null) continue;
            if (!target.isActiveAndEnabled) continue;
            if (tower.Weapon.CanAttack(target.transform))
            {
                tower.SetTarget(target);
                break;
            }
        }
    }

    public void AddTower(Tower tower)
    {
        _towers.Add(tower);
        tower.TargetUnfocus.AddListener(FindTargetTo);
    }
}
