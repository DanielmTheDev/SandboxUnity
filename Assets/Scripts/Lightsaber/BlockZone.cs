using Extensions;
using UnityEngine;

namespace Lightsaber
{
    [RequireComponent(typeof(AudioSource))]
    public class BlockZone : MonoBehaviour
    {
        public float randomnessFactor = 50;

        public AudioClip[] blockSounds;
        public Animator animator;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                animator.SetOneOfTriggers(AnimatorExtensions.Block1Trigger, AnimatorExtensions.Block2Trigger);
                _audioSource.PlayRandomOneShot(blockSounds);
                RedirectProjectile(other.gameObject);
            }
        }

        private void RedirectProjectile(GameObject projectile)
        {
            var rb = projectile.GetComponent<Rigidbody>();
            var incomingDirection = rb.velocity.normalized;

            var collisionNormal = (projectile.transform.position - transform.position).normalized;

            var reflectedDirection = Vector3.Reflect(incomingDirection, collisionNormal);

            var randomOffset = new Vector3(
                Random.Range(-randomnessFactor, randomnessFactor),
                Random.Range(-randomnessFactor, randomnessFactor),
                Random.Range(0, randomnessFactor) // Ensuring the z-component is forward-facing
            );

            var newDirection = (reflectedDirection + randomOffset).normalized;

            if (Vector3.Dot(newDirection, transform.forward) < 0)
            {
                newDirection = (reflectedDirection + transform.forward * randomnessFactor).normalized;
            }

            rb.velocity = newDirection * rb.velocity.magnitude;

            projectile.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
