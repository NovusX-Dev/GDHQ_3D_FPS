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

    public float YLook { get; set; }


    void Update()
    {
        var currentRotation = transform.localEulerAngles;

        currentRotation.x = (currentRotation.x > 180f) ? currentRotation.x - 360f : currentRotation.x;
        currentRotation.x -= YLook * _lookSensitivity * Time.deltaTime;

        currentRotation.x = Mathf.Clamp(currentRotation.x, _minLookX, _maxLookX);
       transform.localRotation = Quaternion.AngleAxis(currentRotation.x, Vector3.right);
    }

}
