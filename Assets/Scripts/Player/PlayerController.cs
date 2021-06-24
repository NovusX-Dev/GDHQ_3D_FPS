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
    [SerializeField] float _rotationSensitivity = 5f;

    private bool _isGrounded;
    private bool _canJump;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _yVelocity;
    private float _xRotation;
    private float _yCameraRotate;

    public float YCameraRotate => _yCameraRotate;

    CharacterController _controller;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        _isGrounded = _controller.isGrounded;

        CalculateMovement();
        PlayerRotationY();
    }

    private void CalculateMovement()
    {
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

        GravityCalculations();

        _velocity.y = _yVelocity;

        _velocity = transform.TransformDirection(_velocity);
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void PlayerRotationY()
    {
        var currentRotation = transform.localEulerAngles;
        currentRotation.y += _xRotation * _rotationSensitivity * Time.deltaTime;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);
    }

    private void GravityCalculations()
    {
        if (_yVelocity >= 0)
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }
        else if (_yVelocity > (_jumpForce / 2))
        {
            _yVelocity -= _gravity * Time.deltaTime * (_fallMultiplier - 2f);
        }
        else if (_yVelocity < 0)
        {
            _yVelocity -= _gravity * Time.deltaTime * _fallMultiplier;
        }
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

    public void OnLookX(InputAction.CallbackContext context)
    {
        _xRotation = context.ReadValue<float>();
    }
    public void OnLookY(InputAction.CallbackContext context)
    {
        _yCameraRotate = context.ReadValue<float>();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            switch (Cursor.lockState)
            {
                case(CursorLockMode.Locked):
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
                case(CursorLockMode.None):
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = false;
                    break;
            }
        }
    }

    #endregion
}
