using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthView : MonoBehaviour
{
    [SerializeField] BaseHealth _base;
    [SerializeField] Image healthBar;
    [SerializeField] int _initialHealth;

    private void Awake()
    {
        _initialHealth = _base.Health;
        _base.HealthChanged.AddListener((int i) => 
        { 
            healthBar.fillAmount = (float)i/(float)_initialHealth;
        });
    }
}
