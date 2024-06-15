using UnityEngine;

public class Gun : MonoBehaviour, IAttacker
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 1f;
    private float _nextFireTime;
    private FireRateLimiter _fireRateLimiter;

    private void Awake() => _fireRateLimiter = new(fireRate, () => Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation));

    public void Attack() => _fireRateLimiter.ExecuteLimitedAction();
}