using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour, IAttacker
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 1f;
    private float _nextFireTime;
    private FireRateLimiter _fireRateLimiter;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Awake() => _fireRateLimiter = new(fireRate, GunShot);

    private void GunShot()
    {
        _audioSource.Play();
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    public void Attack() => _fireRateLimiter.ExecuteLimitedAction();
}