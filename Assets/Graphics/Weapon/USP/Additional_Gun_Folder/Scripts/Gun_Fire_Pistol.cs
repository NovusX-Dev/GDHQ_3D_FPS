using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Fire_Pistol : MonoBehaviour
{
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _bulletCasing;
    [SerializeField] private ParticleSystem _muzzleFlashSide;
    [SerializeField] private ParticleSystem _Muzzle_Flash_Front;
    [SerializeField] private AudioClip _gunShotAudioClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool FullAuto;

    private bool _canAutoFire;

    Animator _anim;
    PlayerShooting _playerShooting;

    private void OnEnable()
    {
        //PlayerInputManager.OnShot +=
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerShooting = GetComponentInParent<PlayerShooting>();
    }

    void Update()
    {
        _canAutoFire = _playerShooting.CanShootGun;

        if (_playerShooting.CanShootGun)
        {
            if (FullAuto == false)
            {
                _anim.SetTrigger("Fire");
                _anim.SetBool("Automatic_Fire", false);
            }
            
        }

        if (FullAuto == true)
        {
            _anim.SetBool("Automatic_Fire", _canAutoFire);
            _anim.ResetTrigger("Fire");
        }

        /*if (_playerShooting.CanShootGun)
        {
            if (FullAuto == true)
            {
                _anim.SetBool("Automatic_Fire", false);
            }

            if (FullAuto == false)
            {
                //_anim.SetBool("Fire", false);
                _anim.ResetTrigger("Fire");
            }
        }*/
    }

    public void FireGunParticles()
    {
        _smoke.Play();
        _bulletCasing.Play();
        _muzzleFlashSide.Play();
        _Muzzle_Flash_Front.Play();
        GunFireAudio();
    }

    public void GunFireAudio()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_gunShotAudioClip);
    }
}
