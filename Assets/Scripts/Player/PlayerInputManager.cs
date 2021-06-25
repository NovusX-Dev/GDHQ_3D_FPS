using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour, PlayerControllerActions.IPlayerControlsActions
{
    PlayerController _playerController;
    PlayerCameraController _playerCamera;
    PlayerShooting _playerShooting;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerCamera = GetComponentInChildren<PlayerCameraController>();
        _playerShooting = GetComponent<PlayerShooting>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _playerController.MovePlayer(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerController.AllowJump(true);
        }
    }

    public void OnLookX(InputAction.CallbackContext context)
    {
        _playerController.RotatePlayer(context.ReadValue<float>());
    }

    public void OnLookY(InputAction.CallbackContext context)
    {
        //_playerCamera.SetYLook(context.ReadValue<float>());
        _playerCamera.YLook = context.ReadValue<float>();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerController.MouseConfinement();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        _playerShooting.CanShoot(context.ReadValueAsButton());
    }
}
