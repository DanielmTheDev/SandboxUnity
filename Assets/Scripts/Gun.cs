using UnityEngine;

public class Gun : MonoBehaviour, IAttacker
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 1f;
    private float _nextFireTime;

    public void Attack()
    {
        if (Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / fireRate;
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
}