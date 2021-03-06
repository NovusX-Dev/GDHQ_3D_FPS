using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void CharacterDeath(int healthID);

    public static event CharacterDeath OnDeath;

    [SerializeField] private int healthID;
    [SerializeField] private int _maxHealth = 100;

    private int _minHealth;
    private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth < 1)
        {
            OnDeath?.Invoke(healthID);
        }
    }


}
