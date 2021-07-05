using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseRadius : MonoBehaviour
{
    EnemyAIMeshController _enemyMesh;
    SphereCollider _collider;

    private void Awake()
    {
        _enemyMesh = GetComponentInParent<EnemyAIMeshController>();
        _collider = GetComponent<SphereCollider>();
    }

    void Start()
    {
        _collider.radius = _enemyMesh.ChaseDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyMesh.StateChanger(EnemyAIMeshController.EnemyState.Chase);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyMesh.StateChanger(EnemyAIMeshController.EnemyState.Idle);
        }
    }

}
