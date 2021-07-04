using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartsHealth : MonoBehaviour
{
    [SerializeField] private bool _head, _body;

    private int _minHealth;
    public EnemyHealth _health;

    private void Start()
    {
        _health = GetComponentInParent<EnemyHealth>();
    }

    public void Damage(int damageAmount)
    {
        if (_head)
        {
            _health.DamageEnemy((int) (damageAmount * Random.Range(1.25f, 1.75f)));
        }
        else if (_body)
        {
            _health.DamageEnemy(damageAmount);
        }
    }

    
}
