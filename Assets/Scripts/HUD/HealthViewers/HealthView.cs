using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(EnemyGameObject))]
public class HealthView : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image healthBar;
    [SerializeField] int _initialHealth;

    private void Awake()
    {
        var enemy = GetComponent<EnemyGameObject>().Enemy;
        canvas.gameObject.SetActive(false);
        _initialHealth = enemy.Health;
        enemy.HealthChanged.AddListener((int i) => 
        {
            canvas.gameObject.SetActive(true);
            healthBar.fillAmount = (float)i/(float)_initialHealth;
        });
    }
}
