using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private float _gravity = 20f;
    [SerializeField] private float _moveSpeed = 5f;

    private Transform _player;
    private Transform _currentTarget;
    private Vector3 _velocity;
    private float _yVelocity;

    CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _currentTarget = _player;
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
            Vector3 direction = _currentTarget.position - transform.position;
            direction.Normalize();
            direction.y = 0f;

            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _moveSpeed;
            //transform.LookAt(_currentTarget);
        }
        else
        {
            _yVelocity -= _gravity;
        }
        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);

    }
}
