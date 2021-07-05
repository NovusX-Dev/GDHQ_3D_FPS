using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private EnemyPartsHealth[] _enemyParts;

    private int _minHealth;
    private int _currentHealth;
    private bool _isDead = false;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public bool IsDead => _isDead;

    void Start()
    {
        _currentHealth = _maxHealth;
        _enemyParts = GetComponentsInChildren<EnemyPartsHealth>();
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
        _isDead = true;
        foreach (var part in _enemyParts)
        {
            part.gameObject.SetActive(false);
        }
        GetComponentInChildren<EnemyChaseRadius>().gameObject.SetActive(false);
        GetComponent<EnemyAnimations>().DeathTriggerAnim();
    }
}
