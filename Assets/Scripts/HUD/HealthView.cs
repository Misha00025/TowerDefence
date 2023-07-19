using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image healthBar;
    [SerializeField] int _initialHealth;

    private void Awake()
    {
        var enemy = GetComponent<Enemy>();
        canvas.enabled = false;
        _initialHealth = enemy.Health;
        enemy.HealthChanged.AddListener((int i) => 
        { 
            canvas.enabled = true;
            healthBar.fillAmount = (float)i/(float)_initialHealth;
        });
    }
}
