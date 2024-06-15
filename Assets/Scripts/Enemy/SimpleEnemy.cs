using System.Collections.Generic;
using System.Linq;
using Enemy;
using Extensions;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class SimpleEnemy : MonoBehaviour, IHittable
{
    public Transform player;

    private bool _isDead;
    private GravityApplier _gravityApplier;
    private NavMeshAgent _navMeshAgent;
    private List<IAttacker> _attackers;
    private IReadOnlyCollection<IAiState> _states;
    private Animator _animator;

    private void Awake()
    {
        _attackers = gameObject.GetComponentsInChildren<IAttacker>().ToList();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _gravityApplier = new(gameObject.GetComponent<CharacterController>());
        _animator = gameObject.GetComponent<Animator>();
        _states = InitializeStates();
    }

    private IReadOnlyCollection<IAiState> InitializeStates() =>
        new List<IAiState>
        {
            // new Shooting(transform, player, _navMeshAgent, _attackers),
            new Chasing(transform, player, _navMeshAgent),
            new Idle(_navMeshAgent, transform)
        };

    private void Update()
    {
        if (_isDead)
            return;
        _states
            .First(state => state.CanActivate())
            .PerformUpdate();

        _gravityApplier.ApplyGravity();
    }

    public void OnHit()
    {
        if (_isDead)
            return;

        _isDead = true;
        _navMeshAgent.Reset();
        _animator.SetTrigger(AnimatorExtensions.HitTrigger);
        Destroy(gameObject, 3f);
    }
}