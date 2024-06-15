using UnityEngine;
using static Extensions.AnimatorExtensions;

public class Lightsaber : MonoBehaviour, IAttacker
{
    public Animator animator;
    private FireRateLimiter _fireRateLimiter;

    private void Awake() => _fireRateLimiter = new(1f, () => animator.SetTrigger(SwingTrigger));

    public void Attack() => _fireRateLimiter.ExecuteLimitedAction();
}
