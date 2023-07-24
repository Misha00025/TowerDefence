using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float _dely = 0.5f;

    private Navigator _navigator;

    public void Initialize(Navigator navigator)
    {
        _navigator = navigator;
    }

    public void StartSpawnWave(Wave wave)
    {
        if (wave == null)
            return;
        Queue<EnemyMover> enemies = new Queue<EnemyMover>();
        foreach (EnemyMover enemy in wave.Enemies)
        {
            enemies.Enqueue(enemy);
        }
        StartCoroutine(SpawnWave(enemies));
    }

    private IEnumerator SpawnWave(Queue<EnemyMover> enemies)
    {
        Vector2[] route = _navigator.Route.ToArray();
        while(enemies.Count > 0)
        {
            EnemyMover enemy = enemies.Dequeue();
            enemy.transform.position = route[0];
            enemy.SetRoute(route);
            enemy.gameObject.SetActive(true);
            yield return new WaitForSeconds(_dely);
        }
    }
}
