using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _id;
    [Header("Movement")]
    [SerializeField] float _gravity = 20f;
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] float _fallMultiplier = 3f;

    [Header("Mouse Sensitivity")]
    [SerializeField] float _rotationSensitivity = 5f;

    private bool _isGrounded;
    private bool _canJump;
    private float _xMove, _zMove;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _yVelocity;
    private float _xRotation;
    private float _yCameraRotate;

    public float YCameraRotate => _yCameraRotate;

    CharacterController _controller;
    Health _health;
    Animator _anim;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _health = GetComponent<Health>();
        _anim = GetComponent<Animator>();
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
            _moveDirection = new Vector3(_xMove, 0f, _zMove);
            _velocity = _moveDirection * _moveSpeed;
            _velocity = transform.TransformDirection(_velocity);

            if (_canJump)
            {
                _yVelocity = 0;
                _yVelocity = _jumpForce;
            }
        }
        else
        {
            _canJump = false;
        }

        GravityCalculations();

        _velocity.y = _yVelocity;
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

    #region Input
    public void MovePlayer(Vector2 input)
    {
        _xMove = input.x;
        _zMove = input.y;
    }

    public void AllowJump(bool jump)
    {
        _canJump = jump;
    }

    public void RotatePlayer(float value)
    {
        _xRotation = value;
    }

    public void MouseConfinement()
    {
        switch (Cursor.lockState)
        {
            case(CursorLockMode.Locked):
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case(CursorLockMode.None):
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                break;
        }
    }

    #endregion
}
