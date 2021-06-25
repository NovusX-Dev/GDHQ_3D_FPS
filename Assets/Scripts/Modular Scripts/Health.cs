using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _minHealth;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        
    }

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth < 1)
        {
            Destroy(gameObject);
        }
    }
}
