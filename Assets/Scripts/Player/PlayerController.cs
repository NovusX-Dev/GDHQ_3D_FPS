using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _gravity = 20f;
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] float _fallMultiplier = 3f;

    private bool _isGrounded;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _yVelocity;

    CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        _isGrounded = _controller.isGrounded;

        if (_isGrounded)
        {
            var xDir = Input.GetAxis("Horizontal");
            var zDir = Input.GetAxis("Vertical");

            _moveDirection = new Vector3(xDir, 0f, zDir);
            _velocity = _moveDirection * _moveSpeed;

            if (Input.GetAxis("Jump") > 0f)
            {
                _yVelocity = 0;
                _yVelocity = _jumpForce;
                _velocity.y = _yVelocity;
            }
        }
        
        if (_yVelocity >= 0)
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }
        else if (_yVelocity > (_jumpForce / 2))
        { _yVelocity -= _gravity * Time.deltaTime * (_fallMultiplier -2f);
        }
        else if(_yVelocity < 0)
        { _yVelocity -= _gravity * Time.deltaTime * _fallMultiplier;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);

    }
}
