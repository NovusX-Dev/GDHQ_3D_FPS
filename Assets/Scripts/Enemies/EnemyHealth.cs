using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _minHealth;
    private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void DamageEnemy(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth < 1)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<EnemyAnimations>().IsDead(true);
    }
}
