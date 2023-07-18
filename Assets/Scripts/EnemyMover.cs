using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 1.6f;
    private Queue<Vector2> _route = new Queue<Vector2>();
    private Vector2 _target;
    private Vector3 _direction = Vector3.zero;



    public void SetRoute(Vector2[] route)
    {
        foreach (var pair in route)
        {
            _route.Enqueue(pair);
        }
        _target = _route.Dequeue();
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        _direction = (_target - position).normalized;
    }

    public void Move()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector3 direction = (_target - position).normalized;
        if (Vector3.Distance(direction, _direction) > 0.1f)
        {
            if (_route.Count == 0)
            {
                Destroy(gameObject);
                return;
            }
            _target = _route.Dequeue();
            _direction = (_target - position).normalized;
            return;
        }
        transform.position += direction * _speed * Time.deltaTime;

    }
}
