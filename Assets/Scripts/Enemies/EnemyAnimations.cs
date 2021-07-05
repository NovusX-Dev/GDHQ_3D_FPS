using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] GameObject[] _bloodSplatter;


    Animator _anim;

    private void OnEnable()
    {
        PlayerShooting.onEnemyHit += DamageEffects;
    }

    private void OnDisable()
    {
        PlayerShooting.onEnemyHit -= DamageEffects;
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    public void DamageEffects(Vector3 origin, Quaternion rotation)
    {
        var blood = Instantiate(_bloodSplatter[Random.Range(0, _bloodSplatter.Length)], origin, rotation);
        blood.transform.position = origin;
        Destroy(blood.gameObject, 1f);
    }

    public void WalkingAnim(bool walk)
    {
        _anim.SetBool("walk", walk);
    }

    public void AttackAnim()
    {
        _anim.SetTrigger("attack");
    }

    public void ResetAttackAnim()
    {
        _anim.ResetTrigger("attack");
    }

    public void IsAttackingAnim(bool attack)
    {
        _anim.SetBool("isAttacking", attack);
    }

    public void DeathTriggerAnim()
    {
        _anim.SetTrigger("die");
    }
}
