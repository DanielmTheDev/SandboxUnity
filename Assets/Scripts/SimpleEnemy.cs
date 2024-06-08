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
        _navMeshAgent.SetDestination(player.position);
        _gravityApplier.ApplyGravity();
    }

    public void OnHit()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;
        StartCoroutine(RotateOverTime(rotationDuration, rotationAngle));
        Destroy(gameObject, 3f);
    }

    private IEnumerator RotateOverTime(float duration, float angle)
    {
        var elapsed = 0f;
        var initialRotation = transform.rotation;
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