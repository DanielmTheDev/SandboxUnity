using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class SimpleEnemy : MonoBehaviour, IHittable
{
    public float rotationDuration = 2f;
    public float rotationAngle = 90f;
    public Transform pivot;
    public Transform player;

    private bool _isDead;
    private GravityApplier _gravityApplier;
    private NavMeshAgent _navMeshAgent;
    private List<IAttacker> _attackers;
    private IReadOnlyCollection<IAiState> _states;

    private void Awake()
    {
        _attackers = gameObject.GetComponentsInChildren<IAttacker>().ToList();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _gravityApplier = new(gameObject.GetComponent<CharacterController>());
        _states = InitializeStates();
    }

    private IReadOnlyCollection<IAiState> InitializeStates() =>
        new List<IAiState>
        {
            new Shooting(transform, player, _navMeshAgent, _attackers),
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
        ResetNavMeshAgent();
        StartCoroutine(RotateOverTime(rotationDuration, rotationAngle));
        Destroy(gameObject, 3f);
    }

    private void ResetNavMeshAgent()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.isStopped = true;
    }

    private IEnumerator RotateOverTime(float duration, float angle)
    {
        var elapsed = 0f;
        var initialRotation = pivot.transform.rotation;
        var targetRotation = initialRotation * Quaternion.Euler(0, 0, angle);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            pivot.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsed / duration);
            yield return null;
        }

        pivot.transform.rotation = targetRotation;
    }
}