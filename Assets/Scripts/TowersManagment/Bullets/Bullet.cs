using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speedModifier = 1f;
    [SerializeField] private float _damageModifier = 1f;
    [SerializeField] private DamageInfo _damageInfo;

    public void LaunchTo(Vector2 direction, float maxDistance, int damage, float speed = 1f)
    {
        StartCoroutine(PersecuteTarget(direction, maxDistance, damage, speed));
    }

    public IEnumerator PersecuteTarget(Vector2 direction, float maxDistance, int damage, float speed = 1f)
    {
        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        float distance = 0;
        Enemy enemy;
        while (distance < maxDistance)
        {
            float delta =  speed * _speedModifier * Time.deltaTime;
            transform.position += (Vector3)(direction * delta);
            distance += delta;
            if (TryFindEnemy(direction, delta, out enemy))
            {
                Vector2 damageDirection = direction * -1;
                enemy.TakeDamage((int)(damage*_damageModifier), new DamageInfo(_damageInfo, damageDirection.normalized));
                break;
            }
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private bool TryFindEnemy(Vector2 direction, float delta, out Enemy enemy)
    {
        enemy = null;

        Ray2D ray2D = new Ray2D() { direction = direction };
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, delta);
        if (hit.collider == null)
            return false;        
        return hit.collider.TryGetComponent<Enemy>(out enemy);
    }
}
