using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerControllerActions.IPlayerControlsActions
{
    [SerializeField] float _gravity = 20f;
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] float _fallMultiplier = 3f;
    [SerializeField] PlayerInput _playerInput;

    private bool _isGrounded;
    private bool _canJump;
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
            _velocity = _moveDirection * _moveSpeed;

            if (_canJump)
            {
                _yVelocity = 0;
                _yVelocity = _jumpForce;
                _velocity.y = _yVelocity;
            }
        }
        else
        {
            _canJump = false;
        }
        
        if (_yVelocity >= 0)
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }
        else if (_yVelocity > (_jumpForce / 2))
        { 
            _yVelocity -= _gravity * Time.deltaTime * (_fallMultiplier -2f);
        }
        else if(_yVelocity < 0)
        { 
            _yVelocity -= _gravity * Time.deltaTime * _fallMultiplier;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);

    }

    #region Player Input Actions
    public void OnMovement(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(input.x, 0f, input.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _canJump = true;
        }
    }
    #endregion
}
