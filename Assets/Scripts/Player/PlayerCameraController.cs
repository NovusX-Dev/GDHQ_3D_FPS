using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float _lookSensitivity = 5f;
    [SerializeField] private float _minLookX = 16f;
    [SerializeField] private float _maxLookX = -20f;
    [SerializeField] PlayerController _player;

    private float _yLook;

    void Update()
    {
        _yLook = _player.YCameraRotate;
        var currentRotation = transform.localEulerAngles;
        currentRotation.x -= _yLook * _lookSensitivity * Time.deltaTime;
       // Mathf.Clamp(currentRotation.x, _minLookX, _maxLookX);
        transform.localRotation = Quaternion.AngleAxis(currentRotation.x, Vector3.right);
       // Mathf.Clamp(transform.localEulerAngles.x, _minLookX, _maxLookX);
    }

}
