using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float _shootingDistance = 100f;
    [SerializeField] private int _attackPower = 5;
    [SerializeField] private float _fireRate = 0.15f;

    private float _nextFire;
    private bool _canShoot;

    Ray _ray;


    void Update()
    {
        if (_canShoot && Time.time > _nextFire)
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
                Debug.Log("Shooting blanks");
            }

            _nextFire = Time.time + _fireRate;
        }
        
    }

    public void CanShoot(bool shoot)
    {
        _canShoot = shoot;
    }

}
