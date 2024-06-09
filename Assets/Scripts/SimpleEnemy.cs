using System.Collections;
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

    private void Awake()
    {
        var characterController = gameObject.GetComponent<CharacterController>();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _gravityApplier = new(characterController);
    }

    private void Update()
    {
        if (IsPlayerInLineOfSight() && IsPlayerInRange())
        {
            _navMeshAgent.SetDestination(player.position);
        }
        else
        {
            _navMeshAgent.SetDestination(gameObject.transform.position);
        }

        _gravityApplier.ApplyGravity();
    }

    private bool IsPlayerInRange()
        => Vector3.Distance(gameObject.transform.position, player.position) < 7f;

    private bool IsPlayerInLineOfSight()
    {
        var isPlayerInFront = Vector3.Angle(transform.forward, player.position - transform.position) < 90f;
        var hasHitSomething = Physics.Linecast(transform.position, player.position, out var hit);
        var isVisionToPlayerClear = hasHitSomething && hit.collider.CompareTag("Player");
        return isPlayerInFront && isVisionToPlayerClear;
    }

    public void OnHit()
    {
        if (_isDead)
        {
            return;
        }

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