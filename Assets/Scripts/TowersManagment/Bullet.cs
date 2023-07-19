using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;

    public IEnumerator PersecuteTarget(Enemy enemy, int damage)
    {
        while(enemy != null && Vector2.Distance(transform.position, enemy.transform.position) > 0.1f)
        {
            Vector3 dimention = (enemy.transform.position - transform.position).normalized;
            transform.position += dimention * _speed * Time.deltaTime;
            //Vector2.MoveTowards(transform.position, enemy.transform.position, _speed * Time.deltaTime);
            yield return null;
        }
        if (enemy != null)
            enemy.TakeDamage(damage);
        gameObject.SetActive(false);
    }
}
