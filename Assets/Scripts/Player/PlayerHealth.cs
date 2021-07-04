using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        UIManager.Instance.UpdateHealth(_currentHealth, _maxHealth);
    }

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth < 1)
        {
            Die();
        }

        UIManager.Instance.UpdateHealth(_currentHealth, _maxHealth);
    }

    private void Die()
    {

    }
}
