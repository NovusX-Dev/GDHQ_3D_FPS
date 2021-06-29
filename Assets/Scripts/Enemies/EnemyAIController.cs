using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public enum EnemyState { Idle, Chase, Attack };
    [SerializeField] private EnemyState _enemyState;

    [Header ("Movement")]
    [SerializeField] private float _gravity = 20f;
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Attack")] 
    [SerializeField] private int _attackPower = 15;
    [SerializeField] private float _attackDistance = 1f;
    [SerializeField] private float _attackRate = 0.5f;

    [Header ("References")]
    [Tooltip ("Must be a sphere collider")] [SerializeField] SphereCollider _collider;

    private Transform _player;
    private Transform _currentTarget;
    private Vector3 _velocity;
    private float _yVelocity;
    private Health _currentAttackTarget;
    private float _nextAttack = -1f;

    CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        _enemyState = EnemyState.Chase;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_player == null)
        {
            Debug.LogError("Player not found");
        }
        else
        {
            _currentTarget = _player;
        }

        if (_collider == null)
        {
            Debug.LogError("No trigger collider detected on enemy " + gameObject.name);
        }
        else
        {
            _collider.radius = _attackDistance;
        }
    }

    void Update()
    {
        switch (_enemyState)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.Chase:
                CalculateMovement();
                break;

            case EnemyState.Attack:
                Attack();
                break;
        }

    }

    private void CalculateMovement()
    {
        if (_controller.isGrounded)
        {
            Vector3 direction = _currentTarget.position - transform.position;
            direction.Normalize();
            direction.y = 0f;

            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _moveSpeed;
        }
        else
        {
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Attack()
    {
        if (Time.time > _nextAttack)
        {
            _currentAttackTarget.Damage(_attackPower);
            _nextAttack = Time.time + _attackRate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentAttackTarget = _player.GetComponent<Health>();

            if (_currentAttackTarget != null)
            {
                _enemyState = EnemyState.Attack;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentAttackTarget = null;
            _enemyState = EnemyState.Chase;
        }
    }
}
