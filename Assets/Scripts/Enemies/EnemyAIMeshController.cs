using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMeshController : MonoBehaviour
{
     public enum EnemyState { Idle, Chase, Attack };
    [SerializeField] private EnemyState _enemyState;

    [Header ("Movement")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _chaseDistance = 50f;

    [Header("Attack")] 
    [SerializeField] private int _attackPower = 15;
    [SerializeField] private float _attackDistance = 1f;
    //[SerializeField] private float _attackRate = 0.5f;

    [Header ("References")]
    [Tooltip ("Must be a sphere collider")] [SerializeField] SphereCollider _collider;

    private Transform _player;
    private Transform _currentTarget;
    private PlayerHealth _currentAttackTarget;
    //private float _nextAttack = -1f;

    public float ChaseDistance => _chaseDistance;

    NavMeshAgent _navMesh;
    EnemyAnimations _animations;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _animations = GetComponent<EnemyAnimations>();
    }

    void Start()
    {
        _navMesh.speed = _moveSpeed;
        _navMesh.stoppingDistance = _attackDistance;
        _enemyState = EnemyState.Idle;

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_player == null)
        {
            Debug.LogError("Player not found");
        }
        else
        {
            _currentTarget = _player;
        }

        if (_collider == null)
        {
            Debug.LogError("No trigger collider detected on enemy " + gameObject.name);
        }
        else
        {
            _collider.radius = _attackDistance;
        }
    }

    void Update()
    {

        switch (_enemyState)
        {
            case EnemyState.Idle:
                _animations.ResetAttackAnim ();
                _animations.IsAttackingAnim(false);
                _animations.WalkingAnim(false);
                break;

            case EnemyState.Chase:
                _animations.ResetAttackAnim ();
                _animations.IsAttackingAnim(false);
                _animations.WalkingAnim(true);
                CalculateMovement();
                break;

            case EnemyState.Attack:
                _animations.IsAttackingAnim(true);
                AttackAnimation();
                break;
        }
    }

    private void CalculateMovement()
    {
        _navMesh.SetDestination(_currentTarget.position);
    }

    private void AttackAnimation()
    {
        _animations.WalkingAnim(false);
        _animations.AttackAnim();
    }

    private void Attack()
    {
        if (_currentAttackTarget != null) 
        {
            _currentAttackTarget.Damage(_attackPower);
            UIManager.Instance.ActivateSplatter();
            //_nextAttack = Time.time + _attackRate;
        }
    }

    public void StateChanger(EnemyState state)
    { 
        if(_currentAttackTarget != null) return;
        _enemyState = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentAttackTarget = _player.GetComponent<PlayerHealth>();

            if (_currentAttackTarget != null)
            {
                _enemyState = EnemyState.Attack;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentAttackTarget = null;
            _animations.ResetAttackAnim ();
        }
    }

}
