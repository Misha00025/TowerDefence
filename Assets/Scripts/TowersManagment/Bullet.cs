using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;

    public IEnumerator PersecuteTarget(Vector2 dimention, float maxDistance, int damage)
    {
        transform.rotation = Quaternion.FromToRotation(Vector2.up, dimention);
        float distance = 0;
        Enemy enemy = null;
        while (distance < maxDistance)
        {
            float delta = _speed * Time.deltaTime;
            if (TryFindEnemy(dimention, delta, out enemy))
            {
                enemy.TakeDamage(damage);
                break;
            }
            transform.position += (Vector3)(dimention * delta);
            distance += delta;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private bool TryFindEnemy(Vector2 direcntion, float delta, out Enemy enemy)
    {
        enemy = null;

        Ray2D ray2D = new Ray2D() { direction = direcntion };
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direcntion, delta);
        if (hit.collider == null)
            return false;        
        return hit.collider.TryGetComponent<Enemy>(out enemy);
    }
}
