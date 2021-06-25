using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float _shootingDistance = 100f;
    [SerializeField] private int _attackPower = 5;

    Ray _ray;

    private bool _canShoot;

    void Update()
    {
        if (_canShoot)
        {
            _ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
            if (Physics.Raycast(_ray, out var hit, _shootingDistance))
            {
                var target = hit.transform.GetComponent<Health>();
                if (target != null)
                {
                    target.Damage(_attackPower);
                }
            }
            else
            {
                Debug.Log("Shooting thin air");
            }
        }
        
    }

    public void CanShoot(bool shoot)
    {
        _canShoot = shoot;
    }

}
