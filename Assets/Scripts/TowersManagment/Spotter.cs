using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Spotter
{
    [SerializeField] private int _numberOfCorrections = 1;
    [SerializeField] private bool _considerDistance = true;
    [SerializeField] private bool _considerSpeed = true;

    private Vector2 _lastPosition;
    private Vector2 _direction;
    private float _speed;
    private GameObject _lastTarget;

    private float _maxDistance;

    public void SetMaxDistance(float maxDistance)
    {
        _maxDistance = maxDistance;
    }

    public void Observe(Transform target)
    {
        if (_lastTarget == target.gameObject)
        {
            _direction = CalculateDirection(_lastPosition, target.position);
            _speed = Vector2.Distance(_lastPosition, target.position)/Time.deltaTime;
        }
        _lastPosition = target.position;
        _lastTarget = target.gameObject;
    }

    public Vector2 SpotDirectionFromTo(Transform from, Transform target, float bulletSpeed)
    {
        if (from == null) throw new Exception("From is null");
        if (target == null) throw new Exception("Target is null");
        //if (_lastTarget == null) throw new Exception("LastTarget is null");

        Vector2 direction = CalculateDirection(from.position, target.position);
        Vector2 correctDirection = direction;

        if (_lastTarget != null)
            if (_lastTarget.gameObject == target.gameObject || _lastPosition != Vector2.zero)
            {
                Vector2 targetDirection = CalculateDirection(_lastPosition, target.position);
                float speed = Vector2.Distance(_lastPosition, target.position);
                if (!_considerSpeed) speed = 1;
                float distance = Vector2.Distance(from.position, target.position);
                Vector2 expectedPosition =
                    CalculateExpectedPosition(from.position, target.position, distance, bulletSpeed, _numberOfCorrections);
                Debug.Log($"Target Direction: {targetDirection}; Speed: {speed}; Distance: {distance}");
                correctDirection = CalculateDirection(from.position, expectedPosition);
            }
        return correctDirection.normalized;
    }

    private Vector2 CalculateExpectedPosition(Vector2 from, Vector2 current, float expectedDistance, float bulletSpeed, int iterations)
    {
        float timeToHit = expectedDistance / bulletSpeed;
        if (!_considerDistance) timeToHit = 1;
        Vector2 delta = _direction * _speed * timeToHit;
        Vector2 expectedPosition = current + delta;
        float distance = Vector2.Distance(from, expectedPosition);
        if (distance > _maxDistance)
            return current;
        if (iterations <= 1)
            return expectedPosition;
        iterations--;
        return CalculateExpectedPosition(from, current, distance, bulletSpeed, iterations);
    }

    private Vector2 CalculateDirection(Vector3 from, Vector3 target)
    {
        Vector3 direction3 = target - from;
        Vector2 direction = new Vector2(direction3.x, direction3.y).normalized;
        return direction;
    }
}
