using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowersController : MonoBehaviour
{
    [SerializeField] private List<Tower> _towers = new List<Tower>();
    private List<Enemy> _enemiesInWave = new List<Enemy>();

    private void Update()
    {
        if (_enemiesInWave == null) return;
        Debug.Log($"Current enemies count: {_enemiesInWave.Count}");
        if (_enemiesInWave.Count == 0) return;

        foreach (var tower in _towers)
        {
            Debug.Log($"TryAttack");
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
        _enemiesInWave.Clear();
        foreach (var enemy in list)
        {
            _enemiesInWave.Add(enemy.Enemy);
        }
    }

    public void FindTargetTo(Tower tower)
    {
        //if (tower.Target == null && _enemiesInWave.Contains(tower.Target)) _enemiesInWave.Remove(tower.Target);
        Debug.Log($"Tower '{tower.name}' find target");
        foreach (var target in _enemiesInWave)
        {
            if (target == null) continue;
            if (!target.isActiveAndEnabled) continue;
            if (tower.Weapon.CanAttack(target))
            {
                tower.SetTarget(target);
                break;
            }
        }
    }

    public void RemoveReadyTo(Wave wave)
    {

    }

    public void AddTower(Tower tower)
    {
        _towers.Add(tower);
        tower.TargetUnfocus.AddListener(FindTargetTo);
    }
}
