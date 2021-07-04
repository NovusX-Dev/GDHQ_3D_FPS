using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseRadius : MonoBehaviour
{
    EnemyAIController _enemy;
    SphereCollider _collider;

    private void Awake()
    {
        _enemy = GetComponentInParent<EnemyAIController>();
        _collider = GetComponent<SphereCollider>();
    }

    void Start()
    {
        _collider.radius = _enemy.ChaseDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy.StateChanger(EnemyAIController.EnemyState.Chase);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy.StateChanger(EnemyAIController.EnemyState.Idle);
        }
    }

}
