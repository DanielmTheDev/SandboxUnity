using UnityEngine;

namespace Lightsaber
{
    [RequireComponent(typeof(AudioSource))]
    public class BlockZone : MonoBehaviour
    {
        public float randomnessFactor = 50;

        public AudioClip[] blockSounds;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                _audioSource.PlayRandomOneShot(blockSounds);
                RedirectProjectile(other.gameObject);
            }
        }

        private void RedirectProjectile(GameObject projectile)
        {
            var rb = projectile.GetComponent<Rigidbody>();
            var incomingDirection = rb.velocity.normalized;

            var reflectedDirection = Vector3.Reflect(incomingDirection, incomingDirection);

            var randomOffset = new Vector3(
                Random.Range(-randomnessFactor, randomnessFactor),
                Random.Range(-randomnessFactor, randomnessFactor),
                Random.Range(-randomnessFactor, randomnessFactor)
            );

            var newDirection = (reflectedDirection + randomOffset).normalized;

            rb.velocity = newDirection * rb.velocity.magnitude;
        }
    }
}
