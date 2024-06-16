using UnityEngine;

namespace Lightsaber
{
    public class BlockZone : MonoBehaviour
    {
        public float randomnessFactor = 50;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                RedirectProjectile(other.gameObject);
            }
        }

        private void RedirectProjectile(GameObject projectile)
        {
            Debug.Log("Entered");
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
