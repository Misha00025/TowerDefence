using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 1.6f;
    private Queue<Vector2> _route = new Queue<Vector2>();
    private Vector2 _target;
    private Vector3 _direction = Vector3.up;
    private Enemy _enemy;
    public Enemy Enemy => _enemy;

    public UnityEvent<int> FinishAlive = new UnityEvent<int>();

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void SetRoute(Vector2[] route)
    {
        foreach (var pair in route)
        {
            _route.Enqueue(pair);
        }
        _target = _route.Dequeue();
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    public void Move()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        //Vector3 direction = (_target - position).normalized;
        if (Vector3.Distance(position, _target) < 0.1f)
        {
            if (_route.Count == 0)
            {
                FinishAlive.Invoke(Enemy.Health);
                Destroy(gameObject);
                return;
            }
            _target = _route.Dequeue();
            _direction = (_target - position).normalized;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90);
            return;
        }
        transform.position += _direction * _speed * Time.deltaTime;

    }
}
