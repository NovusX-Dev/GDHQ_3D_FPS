using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] GameObject[] _bloodSplatter;

    private void OnEnable()
    {
        PlayerShooting.onEnemyHit += DamageEffects;
    }

    private void OnDisable()
    {
        PlayerShooting.onEnemyHit -= DamageEffects;
    }


    public void DamageEffects(Vector3 origin, Quaternion rotation)
    {
        var blood = Instantiate(_bloodSplatter[Random.Range(0, _bloodSplatter.Length)], origin, rotation);
        blood.transform.position = origin;
        Destroy(blood.gameObject, 1f);
    }
}
