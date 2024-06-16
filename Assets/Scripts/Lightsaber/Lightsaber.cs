using Extensions;
using UnityEngine;
using static Extensions.AnimatorExtensions;

namespace Lightsaber
{
    public class Lightsaber : MonoBehaviour, IAttacker
    {
        public Animator animator;
        public AudioClip[] swingSounds;
        public AudioClip[] clashSounds;
        public GameObject sparksEffect;

        private FireRateLimiter _fireRateLimiter;
        private AudioSource _audioSource;

        private void Awake()
        {
            _fireRateLimiter = new(1f, AttackInner);
            _audioSource = GetComponent<AudioSource>();
        }

        public void Attack() => _fireRateLimiter.ExecuteLimitedAction();

        private void AttackInner()
        {
            _audioSource.PlayRandomOneShot(swingSounds);
            animator.SetTrigger(SwingTrigger);
        }

        private void OnTriggerEnter(Collider other)
        {
            _audioSource.PlayRandomOneShot(clashSounds);
            SpawnSparks(other);
            other.gameObject.ExecuteHittableIfAnyTagMatches("Enemy", "Player");
        }

        private void SpawnSparks(Collider other)
        {
            var collisionPosition = other.ClosestPoint(transform.position);
            var effect = Instantiate(sparksEffect, collisionPosition, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}

public static class AudioSourceExtensions
{
    public static void PlayRandomOneShot(this AudioSource source, AudioClip[] clips)
    {
        var index = Random.Range(0, clips.Length);
        source.PlayOneShot(clips[index]);
    }
}