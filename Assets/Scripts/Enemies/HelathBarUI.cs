using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelathBarUI : MonoBehaviour
{
    [SerializeField] Image _healthBar;

    private Health _enemyHealth;

    void Start()
    {
        if (_healthBar == null)
        {
            Debug.LogError("Enemy " + transform.parent.name + " health bar is not set in the inspector!");
        }

        _enemyHealth = GetComponentInParent<Health>();
    }

    void Update()
    {
        _healthBar.fillAmount = (float)_enemyHealth.CurrentHealth / (float)_enemyHealth.MaxHealth;
    }
}
