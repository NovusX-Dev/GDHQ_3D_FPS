using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSmoothDamp : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed = 10f;

    private void LateUpdate()
    {
        if (_target == null)
        {
            Debug.LogError("IK target for " + gameObject.name + " is not set!");
            return;
        }

        var targetPosition = _target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
        transform.rotation = Quaternion.Euler(_target.transform.rotation.eulerAngles);
    }
}
