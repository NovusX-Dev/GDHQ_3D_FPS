using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Fire_Pistol : MonoBehaviour
{
    [Header("Ammo")] [SerializeField] int _maxAmmo = 25;

    [Header("Particle System")]
    [SerializeField]
    private ParticleSystem _smoke;

    [SerializeField] private ParticleSystem _bulletCasing;
    [SerializeField] private ParticleSystem _muzzleFlashSide;
    [SerializeField] private ParticleSystem _Muzzle_Flash_Front;
    [SerializeField] private AudioClip _gunShotAudioClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool FullAuto;

    private bool _canAutoFire;
    private bool _isReloading;
    private bool _outOfAmmo;
    private int _currentAmmo;

    public bool IsReloading => _isReloading;
    public bool OutOfAmmo => _outOfAmmo;

    Animator _anim;
    PlayerShooting _playerShooting;

    private void OnEnable()
    {
        PlayerInputManager.OnReloadWeapon += ReloadGun;
    }

    private void OnDisable()
    {
        PlayerInputManager.OnReloadWeapon -= ReloadGun;
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerShooting = GetComponentInParent<PlayerShooting>();
        _currentAmmo = _maxAmmo;
        UIManager.Instance.UpdateAmmo(_currentAmmo, _maxAmmo);
    }

    void Update()
    {
        if (_isReloading) return;
        if (_outOfAmmo) return;
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

    }

    private void ReloadGun()
    {
        if (!_isReloading)
        {
            StartCoroutine(ReloadingRoutine());
        }
    }

    public void UseAmmo()
    {
        _currentAmmo--;

        if (_currentAmmo < 1)
        {
            _currentAmmo = 0;
            _outOfAmmo = true;
            _anim.SetTrigger("NoAmmo");
        }

        UIManager.Instance.UpdateAmmo(_currentAmmo, _maxAmmo);
    }

    IEnumerator ReloadingRoutine()
    {
        _anim.SetTrigger("Reload");
        _isReloading = true;
        yield return new WaitForSeconds(2.25f);
        _isReloading = false;
        _currentAmmo = _maxAmmo;
        UIManager.Instance.UpdateAmmo(_currentAmmo, _maxAmmo);
        _outOfAmmo = false;

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
